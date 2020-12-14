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

        public bool getLigneDissipatrice()
        {
            return this.ligneDissipatrice;
        }
        public void SetCurrentPower(double power)
        {
            this.currentPower = power; //est mis a jour dans les producteru, ce qu'ils envoient à la ligne
        }
        public double getMaxPower()
        {
            return this.maxPower;
        }
        public void SetDemandPower(double power)
        {
            this.demandPower = power; //est mis a jour dans les producteru, ce qu'ils envoient à la ligne
        }

        public override string ToString()
        {
            return String.Format("La ligne {0} a une puissance max de {1} et consomme {2}", this.lineName, this.maxPower, this.currentPower);
        }

        public double getCurrentConsomation() => this.currentPower;
        public string getNameLine() => lineName;
        public double getDemandPower() => this.demandPower;
    }
}
