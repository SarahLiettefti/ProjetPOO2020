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
            updatePowerOut();
        }
        public string getName()
        {
            return name;
        }
        public Line getLineout()
        {
            return this.outputLine;
        }
        public double getNumberInLine()
        {
            return this.numberInLine;
        }
        public double getPowerOut()
        {
            return this.powerOut;
        }
        public void updatePowerOut()
        {
            this.powerOut = 0;
            foreach (Line inline in inputLines)
            {
                this.powerOut = this.powerOut + inline.getCurrentConsomation();
            }
            outputLine.SetCurrentPower(this.powerOut);
        }

        public void AddInputLine(Line input)
        {
            this.inputLines.Add(input);
            this.numberInLine = this.numberInLine + 1;
            this.powerOut = this.powerOut + input.getCurrentConsomation();
            input.SetNameOutNode(this.name);
        }
        public void updatePowerDemand()//mets a jour les demandes, les lignes et les sorties
        {
            this.powerDemand = outputLine.getDemandPower();
            foreach (Line inline in inputLines)
            {
                inline.SetDemandPower(this.powerDemand/this.numberInLine);//ici divise équitablement
            }
        }

        public double getPowerDemand()
        {
            //updatePowerDemand();
            return this.powerDemand;
        }
    }
}
