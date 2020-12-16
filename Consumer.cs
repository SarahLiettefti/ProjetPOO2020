using System;
using System.Collections.Generic;
using System.Text;

namespace POOProjet
{
    public class Consumer
    {
        public String name;
        public Line inputLine;
        public double powerConsumed;
        public double powerDemand;
        public double powerMissing;
        public static Random generator = new Random();
        public string location;
        public Meteo weather;
        public string ErrorMessage;
        public Consumer(string name, Line inputLine, string localisation)
        {
            this.name = name;
            this.location = localisation;
            this.weather = new Meteo(location);
            this.inputLine = inputLine;
            inputLine.SetNameOutNode(name);
            UpdateConsumption();//par défault l'initialise a random
            UpdateDemand();
            if (powerConsumed >= powerDemand)
            { this.powerMissing = 0; }//ou laisser les lignes gerer ca
            else
            { this.powerMissing = powerDemand - powerConsumed ; } //le manager devra gerrer ca pour que soit proche de 0
            
        }
        public string GetName()
        {
            return name;
        }
        public Line GetLine()
        {
            return inputLine;
        }
        public virtual void UpdateConsumption()
        {
            this.powerConsumed = inputLine.GetCurrentConsomation();
            if (this.powerConsumed > this.powerDemand)
            {
                this.ErrorMessage = String.Format("Le consommateur {0} à une demande de {1} W mais reçoit {2}, ce qui ets trop.", this.name, this.powerDemand, this.powerConsumed);
            }
            else if (this.powerConsumed < this.powerDemand)
            {

                this.ErrorMessage = String.Format("Le consommateur {0} à une demande de {1} W mais reçoit {2}, ce qui n'est pas suffisant.", this.name, this.powerDemand, this.powerConsumed);
            }
            else
            {
                this.ErrorMessage = "";
            }
        }
        public string GetMessage()
        {
            if (this.ErrorMessage == "")
            {
                return String.Format("Le consommateur {0} n'a pas de message d'erreurs", this.name);
            }
            else
            {
                return this.ErrorMessage;
            }
        }
        public virtual double GetConsumption()
        {
            UpdateConsumption();//ici peut update dans le get car va juste regarder sur la ligne quelle est la valeur
            //tant que l'on update pas la ligne le update d'ici reste le mm (je suis pas clair)
            return this.powerConsumed;
        }
        public virtual void UpdateDemand()
        {
            this.powerDemand = generator.Next(80, 100);
            inputLine.SetDemandPower(this.powerDemand);
        }
        public virtual double GetDemand()
        {
            return this.powerDemand;
        }

    }
    public class Town : Consumer
    {
        
        //public Meteo weather;
        private double temperature;
        //public new string location;
        public Town(string name, Line inputLine, string localisation) : base(name, inputLine, localisation)
        {
            this.temperature = weather.GetTemperature();
        }
        public override void UpdateDemand()
        {
            this.powerDemand = generator.Next(80, 100);
            this.weather.UpdateTemperature();
            this.temperature = this.weather.GetTemperature();
            if (temperature < 0)
            {
                this.powerDemand *= 5;
            }
            else if (temperature < 10 && temperature >= 0 )
            {
                this.powerDemand *= 3;
            }
            else if (this.temperature >= 10 && this.temperature < 18)
            {
                this.powerDemand *= 2;
            }
            inputLine.SetDemandPower(this.powerDemand);

        }
    }
    public class Entreprise : Consumer //on va dire que la météo n'influence pas
    {
        public Entreprise(string name, Line inputLine, string localisation) : base(name, inputLine, localisation) {}
    }

    public class Sink : Consumer //on va dire que la météo n'influence pas
    {
        public Sink(string name, Line inputLine, string localisation) : base(name, inputLine, localisation) {
            this.powerDemand = 0;
        }
        public override void UpdateDemand()
        {
            this.powerDemand = 0;
            inputLine.SetDemandPower(this.powerDemand);
        }
    }
}
