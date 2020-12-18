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


        public void UpdatePowerOut()//******************************Le chercheur va devoir modifier cette fonction pour que le noeud distribue le courant comme il veut 
        {
            UpdatePowerIn();
            this.ErrorMessage = "";
            if (this.powerIn > this.powerDemand)
            {
                this.powerDifference = this.powerIn - this.powerDemand;
                this.ErrorMessage = String.Format("Le noeud de distribution {0} reçoit {1} W de la lignes {2} alors qu'il a une demande de {3} W. C'est trop de {4} W.", this.name, this.powerIn, this.inputLine.GetNameLine(), this.powerDemand, this.powerDifference);

            }
            else if (this.powerIn <= this.powerDemand)
            {
                this.powerDifference = 0;
                this.ErrorMessage = String.Format("Le noeud de distribution {0} reçoit {1} W de la lignes {2} alors qu'il a une demande de {3} W. Ce n'est pas assez, il manque {4} W.", this.name, this.powerIn, this.inputLine.GetNameLine(), this.powerDemand, this.powerDifference);

            }
            double powerToGive = this.powerIn;
            foreach (Line outputLine in outputLines)
            {   
                if(this.powerIn == 0)
                {
                    outputLine.SetCurrentPower(0);
                }
                else if (powerToGive <= 0)
                {
                    outputLine.SetCurrentPower(0);
                    powerToGive = 0;
                }
                else if (this.powerIn < this.powerDemand)//Il y a trop de demande part rappot à l'offre
                {
                    if (powerToGive > 0 && powerToGive>=outputLine.GetDemandPower())
                    {
                        outputLine.SetCurrentPower(outputLine.GetDemandPower());
                        powerToGive -= outputLine.GetDemandPower();
                        if (powerToGive <= 0)
                        {
                            powerToGive = 0;
                        }
                    }
                    else
                    {
                        outputLine.SetCurrentPower(powerToGive);
                        powerToGive = 0;
                    }
                    
                }
                else//SI il y a assez d'offre pour la demande
                {
                    if (outputLine.GetLigneDissipatrice()) //si c'est une ligne dissipatrice
                    {
                        if (powerToGive > 0)
                        {
                            double powerdif = this.powerIn - this.powerDemand;//d'office positif puisque powerIn > powerDemand
                            outputLine.SetCurrentPower(powerdif);
                            powerToGive -= powerdif;
                            if (powerToGive <= 0)
                            {
                                powerToGive += powerdif;
                                outputLine.SetCurrentPower(powerToGive);
                                powerToGive = 0;
                            }
                        }
                        else
                        {
                            outputLine.SetCurrentPower(0);
                        }
                    }

                    else if (powerToGive >= outputLine.GetDemandPower() && !(outputLine.GetLigneDissipatrice()))//donc n'est pas dissipatrice et reste assez de power to give pour la demande
                    {
                        outputLine.SetCurrentPower(outputLine.GetDemandPower());
                        powerToGive -= outputLine.GetDemandPower();     
                    }
                    else if(!(outputLine.GetLigneDissipatrice()))
                    {
                        if (powerToGive <= 0)
                        {
                            powerToGive = 0;
                        }
                        outputLine.SetCurrentPower(powerToGive);
                        powerToGive=0;
                    }
                }
            }
        }

        public void UpdatePowerOut1()//******************************Le chercheur va devoir modifier cette fonction pour que le noeud distribue le courant comme il veut 
        {
            UpdatePowerIn();
            this.ErrorMessage = "";
            if (this.powerIn > this.powerDemand)
            {
                this.powerDifference = this.powerIn - this.powerDemand;
                this.ErrorMessage = String.Format("Le noeud de distribution {0} reçoit {1} W de la lignes {2} alors qu'il a une demande de {3} W. C'est trop de {4} W.", this.name, this.powerIn, this.inputLine.GetNameLine(), this.powerDemand, this.powerDifference);

            }
            else if (this.powerIn <= this.powerDemand)
            {
                this.powerDifference = 0;
                this.ErrorMessage = String.Format("Le noeud de distribution {0} reçoit {1} W de la lignes {2} alors qu'il a une demande de {3} W. Ce n'est pas assez, il manque {4} W.", this.name, this.powerIn, this.inputLine.GetNameLine(), this.powerDemand, this.powerDifference);

            }
            double powerToGive = this.powerIn;
            foreach (Line outputLine in outputLines)
            {
                if (this.powerIn == 0)
                {
                    outputLine.SetCurrentPower(0);
                }
                else if (powerToGive > 0)//il reste du courant a donner
                {
                    outputLine.SetCurrentPower(outputLine.GetDemandPower());
                    powerToGive -= outputLine.GetDemandPower();
                }
            }
            if (powerToGive > 0)
            {
                powerToGive = this.powerIn;
                foreach (Line outputLine in outputLines)
                {
                    if (powerToGive > 0)//il reste du courant a donner
                    {
                        if (outputLine.GetLigneDissipatrice()) //si c'est une ligne dissipatrice
                        {
                            if (this.powerDifference > 0)//si il y a plus de powerIn que de demande
                            {
                                outputLine.SetCurrentPower(this.powerDifference);
                                powerToGive -= this.powerDifference;
                            }
                            else
                            {
                                outputLine.SetCurrentPower(0);
                            }
                        }
                        else
                        {
                            if (powerToGive >= outputLine.GetDemandPower())
                            {
                                outputLine.SetCurrentPower(outputLine.GetDemandPower());
                                powerToGive -= outputLine.GetDemandPower();
                            }
                            else
                            {
                                outputLine.SetCurrentPower(powerToGive);
                                powerToGive = 0;
                            }
                        }
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
    }
}
