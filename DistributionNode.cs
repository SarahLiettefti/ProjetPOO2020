using System;
using System.Collections.Generic;
using System.Text;

namespace POOProjet
{
    public class DistributionNode
    {
        public string name;
        public Line inputLine;
        public List<Line> outputLines = new List<Line>();
        public double powerIn;
        public double powerDemand;
        public double powerDifference;
        public double numberOutLine;

        public DistributionNode(string name, Line inputLine)
        {
            this.name = name;
            this.inputLine = inputLine;
            inputLine.SetNameOutNode(name);
            numberOutLine = 0;
            updatePowerIn();
        }

        public List<Line> getoutLines()
        {
            return outputLines;
        }
        public string getName()
        {
            return name;
        }
        public Line getLine()
        {
            return inputLine;
        }
        public double getNumberOutLine()
        {
            return this.numberOutLine;
        }
        public double getPowerIn()
        {
            return this.powerIn;
        }
        public void updatePowerIn()
        {
            this.powerIn = inputLine.getCurrentConsomation();
        }
        
        public void AddOutputLine(Line output)
        {
            this.outputLines.Add(output);
            this.numberOutLine = this.numberOutLine + 1;
            this.powerDemand = this.powerDemand + output.getDemandPower();
            inputLine.SetDemandPower(this.powerDemand);

            updatePowerOut();
        }
        public void updatePowerDemand()//mets a jour les demandes, les lignes et les sorties
        {
            this.powerDemand = 0;
            foreach (Line outline in outputLines)
            {
                this.powerDemand = this.powerDemand + outline.getDemandPower();
            }
            inputLine.SetDemandPower(this.powerDemand);
            updatePowerOut();
        }


        public void updatePowerOut()
        {
            this.powerDifference = this.powerIn - this.powerDemand;//pour l'instant do'ffice positif
            double powerToGive = powerIn;
            foreach (Line outputLine in outputLines)
            {               
                if (powerToGive > 0)//il reste du courant a donner
                {
                    if (outputLine.getLigneDissipatrice()) //si c'est une ligne dissipatrice
                    {
                        outputLine.SetCurrentPower(powerDifference);
                        powerToGive = powerToGive - this.powerDifference;
                    }
                    else
                    {
                        outputLine.SetCurrentPower(outputLine.getDemandPower());
                        powerToGive = powerToGive - outputLine.getDemandPower();
                    }
                }
            }
        }
        
        public double getPowerDemand()
        {
            //updatePowerDemand();
            return this.powerDemand;
        }

        public void updatePowerOutCaca()//ancienen method peut peut-etre réutiliser des trucs
        {
            powerDifference = powerIn - powerDemand;
            double powerToGive = powerIn;
            if (powerIn == powerDemand)
            {//si il y a juste assez donne exactement ce qu'il faut
                foreach (Line outputLine in outputLines)
                {
                    if (powerToGive > 0)
                    {
                        outputLine.SetCurrentPower(outputLine.getDemandPower());
                        powerToGive = powerToGive - outputLine.getDemandPower();
                    }

                }
            }
            else if (powerDifference > 0)//plus de power int que out
            {
                foreach (Line outputLine in outputLines)//il faudrait faire une focntion qui dit a combiend de outline et relié a quel sorte de consomateur
                {
                    if ((outputLine.getDemandPower() == 0) && (powerToGive > 0))
                    {
                        outputLine.SetCurrentPower(powerDifference);
                        powerToGive = powerToGive - powerDifference;
                    }
                    else if (powerToGive > 0)
                    {//pour l'instant mets que ça
                        outputLine.SetCurrentPower(outputLine.getDemandPower());
                        powerToGive = powerToGive - outputLine.getDemandPower();
                    }
                }
            }
            else if (powerDifference < 0)//plus de power int que out
            {
                powerDifference = Math.Abs(powerDifference);
                int NotInfinate = 0;
                while (powerToGive > 0 && NotInfinate < 100)
                {
                    foreach (Line outputLine in outputLines)//il faudrait faire une focntion qui dit a combiend de outline et relié a quel sorte de consomateur
                    {
                        double power = powerDifference / (numberOutLine - NotInfinate);
                        if (power > outputLine.getDemandPower())
                        {
                            outputLine.SetCurrentPower(outputLine.getDemandPower());
                            powerToGive = powerToGive - outputLine.getDemandPower();
                        }
                        else
                        { outputLine.SetCurrentPower(power); }
                    }
                    if (powerToGive > 0)
                    { //a la fin de la boucle si il reste du courant a distribuer reclacule mais cette fois en divisante apr un plsu petit nombre
                        powerToGive = powerIn;
                        NotInfinate = NotInfinate + 1;
                    }
                }
            }
        }
    }
}
