
📚  Projet  Bibliothèque Numérique 

Ce projet est la réalisation d'une application console en C# permettant de gérer une bibliothèque numérique avec différents types de documents.

L’application applique les concepts fondamentaux du développement en C#  : classes abstraites, héritage, exceptions personnalisées, manipulation de collections, sérialisation via fichiers texte, et interface console interactive.

🎯 Objectifs du projet


Créer une hiérarchie de classes représentant des documents numériques.

Utiliser une classe abstraite avec une méthode abstraite obligatoire.

Implémenter plusieurs classes filles (Livre, Magazine, DocumentPDF).

Gérer une collection de documents (ajout, suppression, recherche).

Implémenter une exception personnalisée pour la gestion des erreurs métier.

Lire/écrire des données dans un fichier texte (sérialisation simple).

Construire une application console interactive avec un menu complet.

🧱 Architecture logique 

Le projet est structuré comme suit :

BibliothequeNumerique/
│
├── Document.cs                        # Classe abstraite (Id, Titre, Auteur, Année)
├── Livre.cs                           # Classe Livre dérivée (+ NombrePages)
├── Magazine.cs                        # Classe Magazine dérivée (+ Numéro)
├── DocumentPDF.cs                     # Classe DocumentPDF ( + TailleEnMo )
│
├── DocumentNonTrouveException.cs      # Exception personnalisée exigée
│
├── Bibliotheque.cs                    # Gestion : Ajouter, Supprimer, Rechercher, Afficher
│                                      # + Sauvegarde et Chargement depuis fichier
│
├── Program.cs                         # Application console + menu interactif
│
└── BibliothequeNumerique.csproj       # Fichier du projet .NET




📝 Exemple du menu 
===== MENU BIBLIOTHÈQUE NUMÉRIQUE =====

1. Ajouter un document
2. Afficher tous les documents
3. Rechercher par mot-clé
4. Supprimer un document
5. Sauvegarder dans un fichier
6. Charger depuis un fichier
7. Quitter

Votre choix :
