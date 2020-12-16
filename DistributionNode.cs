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
        public string ErrorMessage;

        public DistributionNode(string name, Line inputLine)
        {
            this.name = name;
            this.inputLine = inputLine;
            inputLine.SetNameOutNode(name);
            numberOutLine = 0;
            UpdatePowerIn();
        }

        public List<Line> GetoutLines()
        {
            return outputLines;
        }
        public string GetName()
        {
            return name;
        }
        public Line GetLine()
        {
            return inputLine;
        }
        public double GetNumberOutLine()
        {
            return this.numberOutLine;
        }
        public double GetPowerIn()
        {
            return this.powerIn;
        }
        public void UpdatePowerIn()
        {
            this.powerIn = inputLine.GetCurrentConsomation();
        }
        
        public void AddOutputLine(Line output)
        {
            this.outputLines.Add(output);
            this.numberOutLine += 1;
            this.powerDemand +=  output.GetDemandPower();
            inputLine.SetDemandPower(this.powerDemand);

            UpdatePowerOut();
        }
        public void UpdatePowerDemand()//mets a jour les demandes, les lignes et les sorties
        {
            this.powerDemand = 0;
            foreach (Line outline in outputLines)
            {
                this.powerDemand += outline.GetDemandPower();
            }
            inputLine.SetDemandPower(this.powerDemand);
            UpdatePowerOut();
        }


        public void UpdatePowerOut()
        {
            this.ErrorMessage = "";
            this.powerDifference = this.powerIn - this.powerDemand;//pour l'instant d'office positif
            if (this.powerIn > this.powerDemand)
            {
                this.ErrorMessage = String.Format("Le noeud de distribution {0} reçoit {1} W de la lignes {2} alors qu'il a une demande de {3} W. C'est trop de {4} W.", this.name, this.powerIn, this.inputLine.GetNameLine(), this.powerDemand, this.powerDifference);

            }
            else if (this.powerIn < this.powerDemand)
            {
                this.ErrorMessage = String.Format("Le noeud de distribution {0} reçoit {1} W de la lignes {2} alors qu'il a une demande de {3} W. Ce n'est pas assez, il manque {4} W.", this.name, this.powerIn, this.inputLine.GetNameLine(), this.powerDemand, this.powerDifference);

            }
            double powerToGive = powerIn;
            foreach (Line outputLine in outputLines)
            {               
                if (powerToGive > 0)//il reste du courant a donner
                {
                    if (outputLine.GetLigneDissipatrice()) //si c'est une ligne dissipatrice
                    {
                        outputLine.SetCurrentPower(powerDifference);
                        powerToGive -= this.powerDifference;
                    }
                    else
                    {
                        outputLine.SetCurrentPower(outputLine.GetDemandPower());
                        powerToGive -= outputLine.GetDemandPower();
                    }
                }
            }
        }
        public string GetMessage()
        {
            if (this.ErrorMessage == "" )
            {
                return String.Format("Le noued de distribution {0} n'a pas de message d'erreur", this.name);
            }
            else
            {
                return this.ErrorMessage;
            }
        }
        public double GetPowerDemand()
        {
            //updatePowerDemand();
            return this.powerDemand;
        }

        public void UpdatePowerOutCaca()//ancienen method peut peut-etre réutiliser des trucs
        {
            powerDifference = powerIn - powerDemand;
            double powerToGive = powerIn;
            if (powerIn == powerDemand)
            {//si il y a juste assez donne exactement ce qu'il faut
                foreach (Line outputLine in outputLines)
                {
                    if (powerToGive > 0)
                    {
                        outputLine.SetCurrentPower(outputLine.GetDemandPower());
                        powerToGive -= outputLine.GetDemandPower();
                    }

                }
            }
            else if (powerDifference > 0)//plus de power int que out
            {
                foreach (Line outputLine in outputLines)//il faudrait faire une focntion qui dit a combiend de outline et relié a quel sorte de consomateur
                {
                    if ((outputLine.GetDemandPower() == 0) && (powerToGive > 0))
                    {
                        outputLine.SetCurrentPower(powerDifference);
                        powerToGive -= powerDifference;
                    }
                    else if (powerToGive > 0)
                    {//pour l'instant mets que ça
                        outputLine.SetCurrentPower(outputLine.GetDemandPower());
                        powerToGive -= outputLine.GetDemandPower();
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
                        if (power > outputLine.GetDemandPower())
                        {
                            outputLine.SetCurrentPower(outputLine.GetDemandPower());
                            powerToGive -= outputLine.GetDemandPower();
                        }
                        else
                        { outputLine.SetCurrentPower(power); }
                    }
                    if (powerToGive > 0)
                    { //a la fin de la boucle si il reste du courant a distribuer reclacule mais cette fois en divisante apr un plsu petit nombre
                        powerToGive = powerIn;
                        NotInfinate += 1;
                    }
                }
            }
        }
    }
}
