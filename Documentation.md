# Documentation

## 1 Ajouter des producteurs ou consommateurs au réseau électrique

### 1.1 Ajout d’un nouveau producteur

Le fichier à utiliser est `PowerPlant.cs`. Il est possible de soit modifier le producteur général `PowerPlant` directement, soit créer un nouveau producteur.
La forme générale de création d’un nouveau producteur est la suivante :

    public class nomProducteur : PowerPlant
    {
        // ajout des fonctions utiles pour le producteur.
    }

### 1.2 Ajout d’un nouveau consommateur

La méthode est identique à celle pour créer un nouveau producteur. Le fichier dans lequel se trouvent les consommateurs est `Consumer.cs`. Pour ajouter un nouveau consommateur, la forme générale est la suivante :

    public class nomConsommateur : Consumer
    {
         // ajout des fonctions utiles pour le consommateur.
    }

## 2 Création d’un réseau

Le réseau est créé dans le fichier `Program.cs`. À condition que les consommateurs et producteurs existent déjà, il est possible de les utiliser ici.

### 2.1 Création d’une nouvelle ligne

La forme générale pour créer une nouvelle ligne dans le réseau est la suivante :

    Line NomLigne = new Line("NomLigne", puissance_maximale);

La `puissance_maximale` est la puissance maximale que la ligne peut supporter.

### 2.2 Création de noeuds

Il y a deux sortes de noeuds qui peuvent être créées : les noeuds de concentration et les noeuds de distribution.

* Un noeud de concentration a plusieurs lignes permet de rassembler plusieurs lignes ayant chacune peu de puissance et les concentrer en une seule ligne.
* Un noeud de distribution permet de distribuer la puissance contenue d’une ligne dans plusieurs lignes.

Pour ajouter des noeuds au réseau électrique, il faut écrire :

    ConcentrationNode NomNoeud = new ConcentrationNode("NomNoeud", ligneSortante); // ajout d’un noeud de concentration
    DistributionNode NomNoeud = new DistributionNode ("NomNoeud", ligneEntrante); // ajout d’un noeud de distribution

### 2.3 Création d’un producteur

Pour ajouter un producteur au réseau, il faut écrire :

    TypeProducteur NomProducteur = new TypeProducteur("NomProducteur", ligneSortante , productionCO2); // éventuellement d’autres arguments peuvent être ajoutés

Les arguments dépendent du type de producteur voulant être créé.


### 2.4 Création d’un consommateur

L’ajout d’un consommateur au réseau se fait ainsi :

    TypeConsommateur NomConsommateur = new TypeConsommateur("NomConsommateur", ligneEntrante , "localisation"); // éventuellement d’autres arguments peuvent être ajoutés

Les arguments dépendent du type de consommateur voulant être créé.

### 2.5 Création d’un dissipateur d'énergie

Pour créer un dissipateur d'énergie dans le réseau, la forme générale est la suivante :

    Sink NomDissipateur = new Sink("NomDissipateur", ligneEntrante , "localisation");

## 3 Météo

Dans l’exemple fourni ici, la météo a été implémentée aléatoirement. Le modèle proposé est de donner des niveaux à l’ensoleillement, au vent et à la température. L’implémentation est laissée libre à l’utilisateur pour l’adapter à ses besoins.

## 4 Utilisation de l’interface

Une fois le réseau créé, l’interface présente plusieurs options :
* `u - update simulation` : met l’état du réseau électrique à jour.
* `p - display network production` : affiche l’état de production des producteur : leur nom, le coût et la production de C02.
* `c - display network consommation` : affiche l’état de consommation des consommateurs : leur nom et la consommation.
* `m - display error messages` : affiche tous les messages d’erreurs pouvant apparaître.
* `a - arrêter un producteur` : permet d’arrêter la production d’une centrale. Il est à noté que les centrales électriques ne s’arrêtent pas instantanément.
* `d - d ́emarrer un producteur` : permet de démarrer un producteur ayant été arrêté auparavant. Il est à noté que les centrales électriques démarrent pas instantanément.
* `v - modification de production` : permet de changer le coefficient de production d’un producteur. Cependant il n’est pas possible de changer la production des centrales nucléaires, des panneaux solaires ainsi que des parcs éoliens.
* `s - stop simulation` : sortir de l’interface et arrêter la simulation du réseau électrique.