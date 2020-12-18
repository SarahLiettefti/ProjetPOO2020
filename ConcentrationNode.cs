using System;
using System.Collections.Generic;
using System.Text;

namespace POOProjet
{
    public class ConcentrationNode
    {
        public string name;
        public Line outputLine; 
        public List<Line> inputLines = new List<Line>();
        public double powerOut;//=power in
        public double powerDemand;
        public double numberInLine;
        public string ErrorMessage;
        public ConcentrationNode(string name, Line outputLine)
        {
            this.name = name;
            this.ErrorMessage = "";
            this.outputLine = outputLine;
            numberInLine = 0;
            UpdatePowerOut();
        }
        public string GetName()
        {
            return name;
        }
        public Line GetLineout()
        {
            return this.outputLine;
        }
        public double GetNumberInLine()
        {
            return this.numberInLine;
        }
        public double GetPowerOut()
        {
            return this.powerOut;
        }
        public void UpdatePowerOut()
        {
            this.powerOut = 0;
            foreach (Line inline in inputLines)
            {
                this.powerOut += inline.GetCurrentConsomation();
            }
            outputLine.SetCurrentPower(this.powerOut);
        }

        public void AddInputLine(Line input)
        {
            this.inputLines.Add(input);
            this.numberInLine ++ ;
            this.powerOut += input.GetCurrentConsomation();
            input.SetNameOutNode(this.name);
        }
        public void UpdatePowerDemand()//mets a jour les demandes, les lignes et les sorties
        {
            this.powerDemand = outputLine.GetDemandPower();
            foreach (Line inline in inputLines)
            {
                inline.SetDemandPower(this.powerDemand*(inline.GetCurrentConsomation() / this.powerOut));//ici divise équitablement
            }
            this.ErrorMessage = "";
            if (this.powerDemand > this.powerOut)
            {
                this.ErrorMessage = String.Format("Le noeud de concentration {0} a une demande ( {1} W) trop grande part rapport à ce quelle reçoit ({2} W)", this.name, this.powerDemand, this.powerOut);
            }
            else if (this.powerDemand < this.powerOut)
            {
                this.ErrorMessage = String.Format("Le noeud de concentration {0} reçoit plus ( {1} W) que ce qu'il demande ({2} W)", this.name, this.powerOut, this.powerDemand);
            }
        }

        public double GetPowerDemand()
        {
            return this.powerDemand;
        }
        public string GetMessage()
        {
            if (this.ErrorMessage == "")
            {
                return String.Format("Le noued de concentration {0} n'a pas de message d'erreur", this.name);
            }
            else
            {
                return this.ErrorMessage;
            }
        }
    }
}
