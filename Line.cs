using System;
using System.Collections.Generic;
using System.Text;

namespace POOProjet
{
    public class Line
    {
        private string lineName;
        private double maxPower;
        private double currentPower;
        private double demandPower;
        private bool ligneDissipatrice;//relié a un dissipateur
        public string NameOutNode;
        public string ErrorMessageDemand = "";
        public string ErrorMessageCurrent = "";

        public Line(string lineName, double maxPower)
        {
            this.lineName = lineName;
            this.maxPower = maxPower;
            this.ligneDissipatrice = false;//par défault faux sauf si le set autrement
        }

        public void SetLigneDissipatrice()
        {
            this.ligneDissipatrice = true;
        }
        public void SetNameOutNode(string nomLigne)
        {
            this.NameOutNode = nomLigne;
        }
        public string GetNameOutNode()
        {
            return this.NameOutNode;
        }

        public bool GetLigneDissipatrice()
        {
            return this.ligneDissipatrice;
        }
        public void SetCurrentPower(double power)
        {
           this.ErrorMessageCurrent = "";
            this.currentPower = power; //est mis a jour dans les producteru, ce qu'ils envoient à la ligne
            if (this.currentPower > this.maxPower)
            {
                this.ErrorMessageCurrent = String.Format(" La ligne {0} est traversé par {1} W alors que ça puissance maximale est de {2} W .", this.lineName, this.currentPower, this.maxPower);
            }
        }
        public double GetMaxPower()
        {
            return this.maxPower;
        }
        public void SetDemandPower(double power)
        {
            this.ErrorMessageDemand = "";
            this.demandPower = power; //est mis a jour dans les producteru, ce qu'ils envoient à la ligne
            if (this.demandPower > this.maxPower)
            {
                this.ErrorMessageDemand =  String.Format(" {0} demande {1} W à {2} alors que ça puissance maximale est de {3} W . La demande a donc été mise au maximum de la ligne" , this.NameOutNode, this.demandPower, this.lineName, this.maxPower);
                this.demandPower = this.maxPower;
            }
        }
        public string GetMessage()
        {
            if (this.ErrorMessageCurrent == "" && this.ErrorMessageDemand == "")
            {
                return String.Format("La ligne {0} n'a pas de message d'erreur", this.lineName);
            }
            else
            {
                return this.ErrorMessageDemand + this.ErrorMessageCurrent;
            }
        }

        public override string ToString()
        {
            return String.Format("La ligne {0} a une puissance max de {1} et consomme {2}", this.lineName, this.maxPower, this.currentPower);
        }

        public double GetCurrentConsomation() => this.currentPower;
        public string GetNameLine() => lineName;
        public double GetDemandPower() => this.demandPower;

        public void ChangeLineName(string newname)
        {
            this.lineName = newname;
        }

        public void ChangeMaxpower (double newmax)
        {
            this.maxPower = newmax;
        }
    }
}
