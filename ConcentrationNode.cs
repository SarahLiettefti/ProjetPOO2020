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

        public ConcentrationNode(string name, Line outputLine)
        {
            this.name = name;
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
                inline.SetDemandPower(this.powerDemand/this.numberInLine);//ici divise équitablement
            }
        }

        public double GetPowerDemand()
        {
            //updatePowerDemand();
            return this.powerDemand;
        }
    }
}
