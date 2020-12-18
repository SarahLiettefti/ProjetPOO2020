using System;
using System.Collections.Generic;

namespace POOProjet
{
    public class Program
    {
        static void Main(string[] args)
        {
            //****Réseau1****
            //Création des lignes
            Line NuclDistBxl = new Line("NuclDisBxl", 3000);
            Line LigneUccle = new Line("LigneUccle", 3000);
            Line LigneWollu = new Line("LigneWollu", 3000);
            Line LigneSink = new Line("Ligne_Sink", 3000);
            LigneSink.SetLigneDissipatrice();
            
            //Création des noeuds
            NuclearPlant NuclBxl = new NuclearPlant("Nucléaire Bxl", NuclDistBxl, 2);
            DistributionNode DistBxl = new DistributionNode("Distribution Bxl", NuclDistBxl);
            Town Wollu = new Town("Wollu", LigneWollu, "Wolluwe");
            Town Uccle = new Town("Uccle", LigneUccle, "Uccle");

            //Création des disipateur
            Sink SinkBxl = new Sink("Dissipateur Bxl", LigneSink, "ici");

            //Connexion des lignes aux noeuds de distributions et de concentrations
            DistBxl.AddOutputLine(LigneUccle);
            DistBxl.AddOutputLine(LigneWollu);
            DistBxl.AddOutputLine(LigneSink);
            List<Line> LigneRéseau1 = new List<Line> { NuclDistBxl, LigneUccle, LigneWollu, LigneSink };
            List<Consumer> ConsoRéseau1 = new List<Consumer> { Uccle , Wollu }; //ne pas y mettre les Sink
            List<PowerPlant> ProducteurRéseau1 = new List<PowerPlant> { NuclBxl };
            List<Sink> SinkRéseau1 = new List<Sink> { SinkBxl };
            List<DistributionNode> DistNoeudRéseau1 = new List<DistributionNode> { DistBxl };
            List<ConcentrationNode> ConcNoeudRéseau1 = new List<ConcentrationNode>();

            ReseauManager Region = new ReseauManager("Région");
            //Region.SetTouteListe(LigneRéseau1, ConsoRéseau1, ProducteurRéseau1, SinkRéseau1, DistNoeudRéseau1, ConcNoeudRéseau1);
            //Region.Show();

            //****RéSEAU 2****
            Line ConcDist = new Line("Concentration-Distribution", 3000);
            Line NuclConcBxl21 = new Line("NuclConBxl", 3000);
            Line NuclConcBxl22 = new Line("NuclConcBxl", 3000);

            NuclearPlant NuclBxl21 = new NuclearPlant("Nucléaire Bxl", NuclConcBxl21, 2);
            //NuclearPlant NuclBxl22 = new NuclearPlant("Nucléaire Bxl", NuclConcBxl22, 2);
            GazStation StationGaz = new GazStation("Station Gaz", NuclConcBxl22, 2);
            //WindPlant EolienBxl = new WindPlant("Parc éolien Bxl", NuclConcBxl22, 2, "Bruxelles");
            DistributionNode DistBxl2 = new DistributionNode("Distribution Bxl", ConcDist);
            ConcentrationNode ConcBxl = new ConcentrationNode("Concentration Bxl", ConcDist);

            DistBxl2.AddOutputLine(LigneUccle);
            DistBxl2.AddOutputLine(LigneWollu);
            DistBxl2.AddOutputLine(LigneSink);
            ConcBxl.AddInputLine(NuclConcBxl21);
            ConcBxl.AddInputLine(NuclConcBxl22);

            List<Line> LigneRéseau2 = new List<Line> { LigneUccle, LigneWollu, LigneSink, ConcDist , NuclConcBxl21, NuclConcBxl22 };
            List<Consumer> ConsoRéseau2 = new List<Consumer> { Uccle, Wollu }; //ne pas y mettre les Sink
            //List<PowerPlant> ProducteurRéseau2 = new List<PowerPlant> { NuclBxl21, EolienBxl };
            List<PowerPlant> ProducteurRéseau2 = new List<PowerPlant> { NuclBxl21, StationGaz };
            List<Sink> SinkRéseau2 = new List<Sink> { SinkBxl };
            List<DistributionNode> DistNoeudRéseau2 = new List<DistributionNode> { DistBxl2 };
            List<ConcentrationNode> ConcNoeudRéseau2 = new List<ConcentrationNode> { ConcBxl };

            ReseauManager Province = new ReseauManager("Province");
            Province.SetTouteListe(LigneRéseau2, ConsoRéseau2, ProducteurRéseau2, SinkRéseau2, DistNoeudRéseau2, ConcNoeudRéseau2);
            Province.Show();//A arranger
        }
    }
}
