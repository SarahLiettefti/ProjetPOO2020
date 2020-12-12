using System;
using System.Collections.Generic;
using System.Text;

namespace POOProjet
{
    public class PowerPlant//héritage plus interressant que interface car peu redéfinir les méthodes et utiliser la méthode venant de la clase mere
    {
        public String name;
        public Line outputLine;
        public double powerProduction;
        public double coefCO2;
        public double coefCout;
        public static Random generator = new Random();
        public double coefStop;
        public Market Bourse = new Market();
        //private double prodtot;
        public PowerPlant(string name, Line outputLine, double coefCO2)
        {
            this.name = name;
            this.outputLine = outputLine;
            this.coefCO2 = coefCO2;
            this.coefCout = Bourse.GetPrixCarburant();
            UpdatePoduction();//par défault l'initialise a random
            coefStop = 1;
            UpdateLine();
        }
        public string getName()
        {
            return name;
        }
        public Line getLine()
        {
            return outputLine;
        }
        public virtual void UpdatePoduction()
        {
            this.powerProduction = generator.Next(80, 100) * coefStop;
            UpdateLine();//met directe à jour la ligne de sortie
        }
        public void UpdateLine()
        {
            
            if (powerProduction > outputLine.getMaxPower())
            {
                outputLine.SetCurrentPower(outputLine.getMaxPower());
                //plus mettre un message d'erreur
            }
            else
            {
                outputLine.SetCurrentPower(powerProduction);
            }
        }
        public virtual double GetProduction()
        {
            return this.powerProduction;
        }
        public double GetCout()
        {
            return (this.powerProduction*coefCout);
        }
        public double GetCO2()
        {
            return (this.powerProduction * coefCO2);
        }
        public virtual void StopProduction()
        {
            this.coefStop = 0;
            this.powerProduction = this.powerProduction * coefStop;
        }
        public virtual void StartProduction()
        {
            this.coefStop = 1;
            this.powerProduction = this.powerProduction * coefStop;
        }
        public virtual void ChangeProduction(double coef)//si veut dimminuer production
        {
            this.powerProduction = this.powerProduction * coef;
        }
    }
    public class GazStation : PowerPlant //celle ci est totalement au hasard
    {
        public GazStation(string name, Line outputLine, double coefCO2) : base (name, outputLine, coefCO2)
        {
        }
        
    }
    public class SolarPlant : PowerPlant
    {
        private string location;
        private Meteo weather;
        private int sun;
        public SolarPlant(string name, Line outputLine, double coefCO2, string location) : base(name, outputLine, coefCO2)
        {
            this.location = location;
            weather = new Meteo(location);
            sun = weather.GetSun();
        }
        
        public override void UpdatePoduction()
        {
            this.powerProduction = generator.Next(80, 100) * coefStop;
            if (sun > 2)
            {
                powerProduction= powerProduction * 5;
            }
            else if (sun < 2)
            {
                powerProduction = powerProduction * 5;
            }
            UpdateLine();

        }
        public override void ChangeProduction(double coef)//car ne peux changer que en diminuant
        {
            Console.WriteLine(String.Format("La production de la central solaire {1} ne peut pas être réduite.", this.name));
        }

    }
    public class NuclearPlant : PowerPlant
    {
        private int count;
        private int inprogress; //3 état : 0 = rien, 1 = en cours de démarrage, 2 = en cours de stoppage
        public NuclearPlant(string name, Line outputLine, double coefCO2) : base(name, outputLine, coefCO2)
        {
            this.count = 0;
            this.inprogress = 0;//on va dire que quand initialise ça a déja démarré
        }
        public override void UpdatePoduction()//car constant
        {
            this.powerProduction = generator.Next(1999, 2001); //pour qd mm un peut changer ? sinon mets juste une valeur
            if (inprogress == 1)//démmarage
            {
                if (this.count < 10) //normalment fait le get production toute les 10secondes don augmente le compteur a chaque fois
                {
                    this.coefStop = this.coefStop + 0.1;
                }
                else
                {
                    this.coefStop = 1;
                    this.count = 0;
                    this.inprogress = 0;
                }
                this.powerProduction = this.powerProduction * coefStop;
            }
            else if (inprogress == 2)//stoppage
            {
                if (this.count < 10) //normalment fait le get production toute les 10secondes don augmente le compteur a chaque fois
                {
                    this.coefStop = this.coefStop - 0.1;
                }
                else
                {
                    this.coefStop = 0;
                    this.count = 0;
                    this.inprogress = 0;
                }
                this.powerProduction = this.powerProduction * coefStop;

            }
            else
            {
               
            }
            UpdateLine();
        }
        public override void StartProduction()
        {
            this.inprogress = 1;
            this.coefStop = 0.1;
            powerProduction = (powerProduction * coefStop )+500 ;
        }
        public override void StopProduction()
        {
            this.inprogress = 2;
            this.coefStop = 0.9;
            powerProduction = (powerProduction * coefStop)+500; //car chere à arreter et a démarer (il faut trouevr comment rendre ça lent)
        }
        
        public override void ChangeProduction(double coef)//car ne peux changer que en diminuant
        {
            Console.WriteLine(String.Format("La production de la central nucléaire {1} ne peut pas être réduite. Elle ne peut que etre stopper ou allumé", this.name));
        }

    }
    public class WindPlant : PowerPlant
    {
        private string location;
        private Meteo weather;
        private int wind;
        public WindPlant(string name, Line outputLine, double coefCO2, string location) : base(name, outputLine, coefCO2)
        {
            this.location = location;
            weather = new Meteo(location);
            wind = weather.GetWind();
        }
        public override void UpdatePoduction()
        {
            if (wind > 2)
            {
                powerProduction = powerProduction * 5;
            }
            else if (wind < 2)
            {
                powerProduction = powerProduction * 5;
            }
            UpdateLine();
        }

    }

    public class AbroadPurchase : PowerPlant //celle ci est totalement au hasard
    {
        public AbroadPurchase(string name, Line outputLine, double coefCO2) : base(name, outputLine, coefCO2)
        {
        }
        //faire en sorte que ça réponde à la demande pour compenser
    }
}
