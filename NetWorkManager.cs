using System;
using System.Collections.Generic;
using System.Text;

namespace POOProjet
{
    public class NetWorkManager
    {
        public string Message;

        public Line NuclDistBxl ;
        public Line LigneUccle ;
        public Line LigneWollu ;
        public Line LigneSink ;

        public NuclearPlant NuclBxl;
        public DistributionNode DistBxl;
        public Town Uccle ;
        public Town Wollu ;
        public Sink SinkBxl;

        public List<Line> touteLignes = new List<Line>();
        public List<Consumer> toutConsommateur = new List<Consumer>();
        public List<PowerPlant> toutProducteur = new List<PowerPlant>();
        public List<Sink> toutSink = new List<Sink>();
        public List<DistributionNode> ToutDistNoeud = new List<DistributionNode>();

        public double productionTot;
        public double costTot;
        public double CO2Tot;
        public NetWorkManager()
        {
            this.Message = "";
            this.NuclDistBxl = new Line("NuclDisBxl", 3000);
            this.LigneUccle = new Line("LigneUccle", 3000);
            this.LigneWollu = new Line("LigneWollu", 3000);
            this.LigneSink = new Line("Ligne_Sink", 3000);
            LigneSink.SetLigneDissipatrice();

            this.NuclBxl = new NuclearPlant("Nucléaire Bxl", NuclDistBxl, 2);
            this.DistBxl = new DistributionNode("Distribution Bxl", NuclDistBxl);
            this.Wollu = new Town("Wollu", LigneWollu, "Wolluwe");
            this.Uccle = new Town("Uccle", LigneUccle, "Uccle");
            
            this.SinkBxl = new Sink("Dissipateur Bxl", LigneSink, "ici");

            DistBxl.AddOutputLine(LigneUccle);
            DistBxl.AddOutputLine(LigneWollu);
            DistBxl.AddOutputLine(LigneSink);  

            touteLignes.Add(LigneSink);
            touteLignes.Add(LigneUccle);
            touteLignes.Add(LigneWollu);
            touteLignes.Add(NuclDistBxl);

            this.toutConsommateur.Add(Uccle);
            this.toutConsommateur.Add(Wollu);

            this.toutSink.Add(SinkBxl);

            this.toutProducteur.Add(NuclBxl);

            this.ToutDistNoeud.Add(DistBxl);
            UpdateProdTot();

        }
        
        public void AddMessage(string mess)
        {
            this.Message = this.Message + "\n" + mess;
        }

        public void ReadMessages()
        {
            Console.WriteLine(this.Message);
        }
        public override string ToString()
        {
            return "Done";
        }
        public void UpdateNetWork()
        {
            foreach (Consumer consomateur in toutConsommateur)
            {
                consomateur.UpdateDemand();//d'abord change la demande, la demande de la ligne est directe changé
            }

            foreach (PowerPlant producteur in toutProducteur)
            {
                producteur.UpdatePoduction();
            }
                foreach (DistributionNode noeud in ToutDistNoeud)
            {
                noeud.UpdatePowerIn();
                noeud.UpdatePowerDemand();
            }
            UpdateProdTot();


        }
        public void Affichage()
        {
            Console.WriteLine("Les centrales : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (PowerPlant producteur in toutProducteur)
            {
                Console.WriteLine("Nom : " + producteur.GetName());
                Console.WriteLine("Production : " + producteur.GetProduction() + " W");
                Console.WriteLine("Ligne de sortie : " + producteur.GetLine().GetNameLine());
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                //Console.WriteLine(String.Format("La centrale {0} produit {1} W et est relié à la ligne {2}", producteur.getName(), producteur.GetProduction(), producteur.getLine().getNameLine()));
            }
            
            Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////////////////////");

            Console.WriteLine("Les noeuds de distribution : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (DistributionNode noeud in ToutDistNoeud)
            {
                Console.WriteLine("Nom : " + noeud.GetName());
                Console.WriteLine("Puissance reçue : " + noeud.GetPowerIn() + " W");
                Console.WriteLine("Demande : " + noeud.GetPowerDemand() + " W");
                Console.WriteLine("Ligne d'entrée : " + noeud.GetLine().GetNameLine());
                Console.WriteLine("Nombre de lignes de sorties : " + noeud.GetNumberOutLine());
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                //Console.WriteLine(String.Format("Le noeud de distribution {0} reçoit {1} W de puissance de {2}. Il a une demande de {3} W à distribué à {4} lignes", noeud.getName(), noeud.getPowerIn(), noeud.getLine().getNameLine(), noeud.getPowerDemand(), noeud.getNumberOutLine()));
            }
            Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////////////////////");

            Console.WriteLine("Les consommateurs : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (Consumer consomateur in toutConsommateur)
            {
                Console.WriteLine("Nom : " + consomateur.GetName());
                Console.WriteLine("Demande : " + consomateur.GetDemand()+ " W");
                Console.WriteLine("Consommation : " + consomateur.GetConsumption() + " W");
                Console.WriteLine("Ligne d'entrée : " + consomateur.GetLine().GetNameLine());
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                //Console.WriteLine(String.Format("La ville {0}, reçoit l'energie par {1} à une demande de {2}, consomme {3} ", consomateur.getName(), consomateur.getLine().getNameLine(), consomateur.GetDemand(), consomateur.GetConsumption()));
            }
            Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////////////////////");

            Console.WriteLine("Les dissipateurs : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (Sink sink in toutSink)
            {
                Console.WriteLine("Nom : " + sink.GetName());
                Console.WriteLine("Demande : " + sink.GetDemand() + " W");
                Console.WriteLine("Consommation : " + sink.GetConsumption() + " W");
                Console.WriteLine("Ligne d'entrée : " + sink.GetLine().GetNameLine());
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                //Console.WriteLine(String.Format("Le dissipateur {0}, reçoit l'energie par {1} à une demande de {2}, consomme {3} ", sink.getName(), sink.getLine().getNameLine(), sink.GetDemand(), sink.GetConsumption()));
            }
            Console.WriteLine("//////////////////////////////////////////////////////////////////////////////////////////////////////");

            Console.WriteLine("Les lignes : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (Line lignes in touteLignes)
            {
                Console.WriteLine("Nom : " + lignes.GetNameLine());
                Console.WriteLine("Demande : " + lignes.GetDemandPower() + " W");
                Console.WriteLine("Consommation : " + lignes.GetCurrentConsomation() + " W");
                Console.WriteLine("Consommation maximale : " + lignes.GetMaxPower() + " W");
                Console.WriteLine("-------------------------------------------------------------------------------------------------------");
                //Console.WriteLine(String.Format("La ligne {0} transfert {1} W et a une demande de {2}", lignes.getNameLine(), lignes.getCurrentConsomation(), lignes.getDemandPower()));
            }

        }
        public void AffichageMini()
        {
            Console.WriteLine("Les centrales : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (PowerPlant producteur in toutProducteur)
            {
                Console.WriteLine("Nom : " + producteur.GetName() + "Production : " + producteur.GetProduction() + " W" );
            }
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Les noeuds de distribution : ");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (DistributionNode noeud in ToutDistNoeud)
            {
                Console.WriteLine("Nom : " + noeud.GetName()+ " Puissance reçue : " + noeud.GetPowerIn() + " W"+ " Demande : " + noeud.GetPowerDemand() + " W"+ "  Nbr lignes de sorties : " + noeud.GetNumberOutLine());
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
        public void AffichageGraphique()
        {
            //Console.WriteLine("Les centrales : ");
            //Console.WriteLine("********************************************************************************************************");
            //Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (PowerPlant producteur in toutProducteur)
            {
                Console.WriteLine( producteur.GetName() + "------->"+ producteur.GetProduction() + " W ------->" + producteur.GetLine().GetNameOutNode());
            
            }
            Console.WriteLine(" ");
            //Console.WriteLine("********************************************************************************************************");
            //Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            //Console.WriteLine("Les noeuds de distribution : ");
            //Console.WriteLine("********************************************************************************************************");
            //Console.WriteLine("-------------------------------------------------------------------------------------------------------");
            foreach (DistributionNode noeud in ToutDistNoeud)
            {
                foreach(Line ligne in noeud.GetoutLines())
                {
                    Console.WriteLine(noeud.GetName() + "-------> demande " + ligne.GetDemandPower() + " W ------->" + " consommation " + ligne.GetCurrentConsomation() + " W ------->" + ligne.GetNameOutNode() );
                }
                
            }
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
    }
}
