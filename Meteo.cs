using System;
using System.Collections.Generic;
using System.Text;

namespace POOProjet
{
    public class Meteo
    {
        private int sun;
        private int wind;
        private int temperature;
        private string location;
        public static Random generator = new Random();

        public Meteo(string location)
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
        public int GetSun()
        {
            return this.sun;
        }
        public void UpdateWind()
        {
            this.wind = generator.Next(1, 5); //il y aurait 5 niveau
        }
        public int GetWind()
        {
            return this.wind;
        }
        public void UpdateTemperature()
        {
            this.temperature = generator.Next(0, 30); //il y aurait 5 niveau
        }
        public int GetTemperature()
        {
            return this.temperature;
        }
    }
}
