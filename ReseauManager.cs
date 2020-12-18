using System;
using System.Collections.Generic;
using System.Text;

namespace POOProjet
{
    class ReseauManager
    {
        public List<Line> touteLignes = new List<Line>();
        public List<Consumer> toutConsommateur = new List<Consumer>();
        public List<PowerPlant> toutProducteur = new List<PowerPlant>();
        public List<Sink> toutSink = new List<Sink>();
        public List<DistributionNode> toutDistNoeud = new List<DistributionNode>();
        public List<ConcentrationNode> toutConcNoeud = new List<ConcentrationNode>();


        public double productionTot;
        public double costTot;
        public double CO2Tot;
        public double consommationTot;
        public string ErrorMessage;
        public bool state;

        public string name;

        public ReseauManager(string name)
        {
            this.name = name;
            this.state = true;
        }

        public void SetTouteListe(List<Line> touteLignes, List<Consumer> toutConsommateur, List<PowerPlant> toutProducteur,List<Sink> toutSink, List<DistributionNode> toutDistNoeud, List<ConcentrationNode> toutConcNoeud)
        {
            this.touteLignes = touteLignes;
            this.toutConsommateur = toutConsommateur;
            this.toutProducteur = toutProducteur;
            this.toutSink = toutSink;
            this.toutDistNoeud = toutDistNoeud;
            this.toutConcNoeud = toutConcNoeud;
        }
        public void SetListeLine(List<Line> touteLignes)
        {
            this.touteLignes = touteLignes;
        }
        public void AddLine(Line ligne)
        {
            this.touteLignes.Add(ligne);
        }
        public void SetListeConsumer(List<Consumer> toutConsommateur)
        {
            this.toutConsommateur = toutConsommateur;
        }
        public void AddConsumer(Consumer consommateur)
        {
            this.toutConsommateur.Add(consommateur);
        }
        public void SetListeProducteur(List<PowerPlant> toutProducteur)
        {
            this.toutProducteur = toutProducteur;
        }
        public void AddProducteur(PowerPlant producteur)
        {
            this.toutProducteur.Add(producteur);
        }
        public void SetListeSink(List<Sink> toutSink)
        {
            this.toutSink = toutSink;
        }
        public void AddSink(Sink sink)
        {
            this.toutSink.Add(sink);
        }
        public void SetListeDistribution(List<DistributionNode> toutDistNoeud)
        {
            this.toutDistNoeud = toutDistNoeud;
        }
        public void AddDistribution(DistributionNode distribution)
        {
            this.toutDistNoeud.Add(distribution);
        }
        public void SetListeConcentration(List<ConcentrationNode> toutConcNoeud)
        {
            this.toutConcNoeud = toutConcNoeud;
        }
        public void AddConcentration(ConcentrationNode concentration)
        {
            this.toutConcNoeud.Add(concentration);
        }
        public void UpdateNetWork()
        {
            this.ErrorMessage = "";
            foreach (Consumer consomateur in toutConsommateur)
            {
                consomateur.UpdateDemand();//d'abord change la demande, la demande de la ligne est directe changé
                if (consomateur.GetMessage() != "")
                {
                    ErrorMessage += consomateur.GetMessage() + "\n" ;
                }
            }

            foreach (PowerPlant producteur in toutProducteur)
            {
                producteur.UpdatePoduction();
                if (producteur.GetMessage() != "")
                {
                    ErrorMessage += producteur.GetMessage() + "\n";
                }

            }
            foreach (DistributionNode noeud in toutDistNoeud)
            {
                noeud.UpdatePowerIn();
                noeud.UpdatePowerDemand();
                if (noeud.GetMessage() != "")
                {
                    ErrorMessage += noeud.GetMessage() + "\n";
                }
            }
            foreach (ConcentrationNode noeud in toutConcNoeud)
            {
                noeud.UpdatePowerOut();
                noeud.UpdatePowerDemand();
                if (noeud.GetMessage() != "")
                {
                    ErrorMessage += noeud.GetMessage() + "\n";
                }
            }
            UpdateProdTot();
            UpdateConsommation();
            foreach (DistributionNode noeud in toutDistNoeud)
            {
                noeud.UpdatePowerIn();
                noeud.UpdatePowerDemand();
                
            }
            foreach (ConcentrationNode noeud in toutConcNoeud)
            {
                noeud.UpdatePowerOut();
                noeud.UpdatePowerDemand();
            }
            UpdateProdTot();
            UpdateConsommation();

            foreach (Sink sink in toutSink)
            {
                if (sink.GetMessage() != "")
                {
                    ErrorMessage += sink.GetMessage() + "\n";
                }
            }
            foreach (Line lignes in touteLignes)
            {
                if (lignes.GetMessage() != "")
                {
                    ErrorMessage += lignes.GetMessage() + "\n";
                }
            }
            if (ErrorMessage == "")
            {
                this.ErrorMessage = String.Format("Le réseau {0} n'a pas de message d'erreur", this.name);
            }

        }
        public void GetMessage()
        {
            Console.WriteLine(this.ErrorMessage);
        }
        public void UpdateProdTot()//permet de calculer la production totale, le cout total et l'emmision CO2 tot
        {
            this.productionTot = 0;
            this.costTot = 0;
            this.CO2Tot = 0;
            foreach (PowerPlant producteur in toutProducteur)
            {
                this.productionTot += producteur.GetProduction();
                this.costTot += producteur.GetCout();
                this.CO2Tot += producteur.GetCO2();
            }
        }

        public void UpdateConsommation()
        {
            this.consommationTot = 0;
            foreach (Consumer consomateur in toutConsommateur)
            {
                this.consommationTot += consomateur.GetConsumption();
            }
        }
        public double GetProductionTotal() => this.productionTot;
        public double GetCoutTotal() => this.costTot;
        public double GetCO2Total() => this.CO2Tot;
        public double GetConsommationTotal() => this.consommationTot;
        public void GetProducteurs()
        {
            int i = 0;
            Console.WriteLine("Choisisez un producteur");
            Console.WriteLine("\ta - Annuler");
            foreach (PowerPlant producteur in toutProducteur)
            {
                Console.WriteLine(String.Format("\t{0} - {1}",i , producteur.GetName()));
                i++;
            }
        }
        public void Affichage()
        {
            Console.WriteLine("Les centrales : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (PowerPlant producteur in toutProducteur)
            {
                Console.WriteLine("Nom : " + producteur.GetName() + "Production : " + producteur.GetProduction() + " W");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Les noeuds de distribution : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (DistributionNode noeud in toutDistNoeud)
            {
                Console.WriteLine("Nom : " + noeud.GetName() + " Puissance reçue : " + noeud.GetPowerIn() + " W" + " Demande : " + noeud.GetPowerDemand() + " W" + "  Nbr lignes de sorties : " + noeud.GetNumberOutLine());
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Les noeuds de concentration : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (ConcentrationNode noeud in toutConcNoeud)
            {
                Console.WriteLine("Nom : " + noeud.GetName() + " Puissance reçue : " + noeud.GetPowerOut() + " W" + " Demande : " + noeud.GetPowerDemand() + " W" + "  Nbr lignes de sorties : " + noeud.GetNumberInLine());
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Les consommateurs : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (Consumer consomateur in toutConsommateur)
            {
                Console.WriteLine("Nom : " + consomateur.GetName() + " Demande : " + consomateur.GetDemand() + " W" + " Consommation : " + consomateur.GetConsumption() + " W");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Les dissipateurs : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (Sink sink in toutSink)
            {
                Console.WriteLine("Nom : " + sink.GetName() + " Demande : " + sink.GetDemand() + " W" + " Consommation : " + sink.GetConsumption() + " W");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Les lignes : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (Line lignes in touteLignes)
            {
                Console.WriteLine("Nom : " + lignes.GetNameLine() + " Consommation : " + lignes.GetCurrentConsomation() + " W" + " Consommation maximale : " + lignes.GetMaxPower() + " W");
            }
            Console.WriteLine("********************************************************************************************************");

        }
        public void AffichageProduction()
        {
            Console.WriteLine("Production des centrales : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (PowerPlant producteur in toutProducteur)
            {
                Console.WriteLine("Nom : " + producteur.GetName() + "Production : " + producteur.GetProduction() + " W" + " Frais de production : " + producteur.GetCout() + " $ Emmission C02 : " + producteur.GetCO2());
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Production total : " + this.productionTot + " W Cout total : " + this.costTot + " $ Emmission CO2 total : " + this.CO2Tot);
        }
        public void AffichageConsommation()
        {
            Console.WriteLine("Consommations des consommateurs et dissipateurs: ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (Consumer consomateur in toutConsommateur)
            {
                Console.WriteLine("Nom : " + consomateur.GetName() + " Demande : " + consomateur.GetDemand() + " W" + " Consommation : " + consomateur.GetConsumption() + " W");
            }
            foreach (Sink sink in toutSink)
            {
                Console.WriteLine("Nom : " + sink.GetName() + " Consommation : " + sink.GetConsumption() + " W");
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Consommation total des consommateur : " + this.consommationTot + " W " );
        }

        public void Show()
        {
            //UpdateNetWork();
            UpdateNetWork();
            Affichage();

            this.state = true;
            while (state)
            {
                Console.WriteLine("Choose an option from the following list:");
                Console.WriteLine("\tu - Update Simulation");
                Console.WriteLine("\tp - Display network production");
                Console.WriteLine("\tc - Display network consommation");
                Console.WriteLine("\tm - Display Error Messages");
                Console.WriteLine("\ta - Arreter un producteur");
                Console.WriteLine("\td - Démarrer un producteur");
                Console.WriteLine("\tv - Modification de production");
                Console.WriteLine("\ts - Stop Simulation");

                switch (Console.ReadLine())
                {
                    case "u":
                        UpdateNetWork();
                        Affichage();
                        break;
                    case "p":
                        AffichageProduction();
                        break;
                    case "c":
                        AffichageConsommation();
                        break;
                    case "m":
                        GetMessage();
                        break;
                    case "a":
                        GetProducteurs();
                        String e = Console.ReadLine();
                        if (e == "a")
                        {
                            break;
                        }
                        else { 
                            int x = Int32.Parse(e);
                            toutProducteur[x].StopProduction();
                            if (toutProducteur[x].type== "Centrale Nucléaire")
                            {
                                Console.WriteLine("La centrale nucléaire va mettre du temps à s'arreter");
                            }
                            break;
                        }
                    case "d":
                        GetProducteurs();
                        String l = Console.ReadLine();
                        if (l == "a")
                        {
                            break;
                        }
                        else
                        {
                            int x = Int32.Parse(l);
                            toutProducteur[x].StartProduction();
                            if (toutProducteur[x].type == "Centrale Nucléaire")
                            {
                                Console.WriteLine("La centrale nucléaire va mettre du temps à démarrer");
                            }
                            break;
                        }
                    case "v":
                        Console.WriteLine("Attention, la production des centrale nucléaire, des parcs éoliens et panneaux solaire ne peuvent être modifier");
                        GetProducteurs();
                        String ll = Console.ReadLine();
                        if (ll == "a")
                        {
                            break;
                        }
                        else
                        {
                            int x = Int32.Parse(ll);
                            Console.WriteLine("De quel coéfficient voulez vous changer la production? utiliser le virgule et pas le pooint");
                            double c = Convert.ToDouble(Console.ReadLine());
                            toutProducteur[x].ChangeProduction(c);
                            if (toutProducteur[x].type == "Centrale Nucléaire")
                            {
                                Console.WriteLine("La production de la centrale nucléaire ne peut pas être changé");
                            }
                            break;
                        }
                    case "s":
                        this.state = false;
                        break;
                }
            }
        }
    }
}
