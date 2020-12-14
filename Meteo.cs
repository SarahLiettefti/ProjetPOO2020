using System;
using System.Collections.Generic;
using System.Text;

namespace POOProjet
{
    public class Meteo
    {
        public double sun;
        public double wind;
        public double temperature;
        public String location;
        public Random generator = new Random();

        public Meteo(String location)
        {
            this.location = location;
            UpdateSun();
            UpdateWind();
            UpdateTemperature();
        }
        public void UpdateSun()
        {
            this.sun = generator.Next(1, 5); //il y aurait 5 niveau
        }
        public double GetSun()
        {
            return this.sun;
        }
        public void UpdateWind()
        {
            this.wind = generator.Next(1, 5); //il y aurait 5 niveau
        }
        public double GetWind()
        {
            return this.wind;
        }
        public void UpdateTemperature()
        {
            this.temperature = generator.Next(1, 30); //il y aurait 5 niveau!!!!!!! ne sutout pas mettre à 0 fait grosse erreur jsp pq
        }
        public double GetTemperature()
        {
            return this.temperature;
        }
    }
}
