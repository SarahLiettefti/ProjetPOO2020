using System;
using System.Collections.Generic;
using System.Text;

namespace POOProjet
{
    public class Market
    {
        private double prixAchat; //"le prix du W" acheté à l'étranger
        private double prixVente; //prix de vente vers l'étranger
        private double prixCarburant;
        public static Random generator = new Random();

        public Market()
        {
            UpdateBourse();
        }
        public void UpdateBourse()
        {
            this.prixAchat = generator.Next(80, 100);
            this.prixVente = generator.Next(70, 90);
            this.prixCarburant = generator.Next(40, 50);
        }

        public double GetPrixAchat()
        {
            return this.prixAchat;
        }
        public double GetPrixVente()
        {
            return this.prixVente;
        }
        public double GetPrixCarburant()
        {
            return this.prixCarburant;
        }
    }
}
