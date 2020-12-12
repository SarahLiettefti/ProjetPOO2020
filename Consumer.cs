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
        public Consumer(string name, Line inputLine)
        {
            this.name = name;
            this.inputLine = inputLine;
            UpdateConsumption();//par défault l'initialise a random
            UpdateDemand();
            if (powerConsumed >= powerDemand)
            { this.powerMissing = 0; }//ou laisser les lignes gerer ca
            else
            { this.powerMissing = powerDemand - powerConsumed ; } //le manager devra gerrer ca pour que soit proche de 0
            
        }
        public string getName()
        {
            return name;
        }
        public Line getLine()
        {
            return inputLine;
        }
        public virtual void UpdateConsumption()
        {
            this.powerConsumed = inputLine.getCurrentConsomation();

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
        private string location;
        private Meteo weather;
        private int temperature;
        public Town(string name, Line inputLine, string location) : base(name, inputLine)
        {
            this.location = location;
            this.weather = new Meteo(location);
            this.temperature = this.weather.GetTemperature();
        }
        public override void UpdateDemand()
        {
            this.powerDemand = generator.Next(80, 100);
            this.weather.UpdateTemperature();
            this.temperature = this.weather.GetTemperature();
            if (this.temperature < 0)
            {
                this.powerDemand = this.powerDemand * 5;
            }
            else if (this.temperature < 10 && this.temperature >= 0 )
            {
                this.powerDemand = this.powerDemand * 3;
            }
            else if (this.temperature >= 10 && this.temperature < 18)
            {
                this.powerDemand = this.powerDemand * 2;
            }
            inputLine.SetDemandPower(this.powerDemand);

        }
    }
    public class Entreprise : Consumer //on va dire que la météo n'influence pas
    {
        public Entreprise(string name, Line inputLine) : base(name, inputLine) {}
    }

    public class Sink : Consumer //on va dire que la météo n'influence pas
    {
        public Sink(string name, Line inputLine) : base(name, inputLine) {
            this.powerDemand = 0;
        }
        public override void UpdateDemand()
        {
            this.powerDemand = 0;
            inputLine.SetDemandPower(this.powerDemand);
        }
    }
}
