using System;

namespace BibliothequeNumerique
{
    class Program
    {
        static void Main(string[] args)
        {
            Bibliotheque biblio = new Bibliotheque();
            string cheminFichier = "bibliotheque_sauvegarde.txt";
            bool quitter = false;

            while (!quitter)
            {
                Console.WriteLine("\n===== MENU BIBLIOTHÈQUE NUMÉRIQUE =====");
                Console.WriteLine("1. Ajouter un document");
                Console.WriteLine("2. Afficher tous les documents");
                Console.WriteLine("3. Rechercher par mot-clé");
                Console.WriteLine("4. Supprimer un document");
                Console.WriteLine("5. Sauvegarder dans un fichier");
                Console.WriteLine("6. Charger depuis un fichier");
                Console.WriteLine("7. Quitter");
                Console.Write("Votre choix : ");

                string? choix = Console.ReadLine();
                Console.WriteLine();

                try
                {
                    switch (choix)
                    {
                        case "1":
                            AjouterDocumentDepuisConsole(biblio);
                            break;

                        case "2":
                            biblio.AfficherTous();
                            break;

                        case "3":
                            Console.Write("Entrez un mot-clé : ");
                            string motCle = Console.ReadLine() ?? "";
                            var resultats = biblio.Rechercher(motCle);
                            Console.WriteLine($"\n {resultats.Count} document trouvé :");
                            foreach (var d in resultats)
                            {
                                d.AfficherDetails();
                            }
                            break;

                        case "4":
                            Console.Write("Entrez l'Id du document à supprimer : ");
                            string idStr = Console.ReadLine() ?? "";

                            if (!Guid.TryParse(idStr, out Guid id))
                            {
                                Console.WriteLine("Id invalide.");
                            }
                            else
                            {
                                biblio.SupprimerDocument(id);
                            }
                            break;

                        case "5":
                            biblio.Sauvegarder(cheminFichier);
                            break;

                        case "6":
                            biblio.Charger(cheminFichier);
                            break;

                        case "7":
                            quitter = true;
                            Console.WriteLine(" Au revoir !");
                            break;

                        default:
                            Console.WriteLine("Choix invalide.");
                            break;
                    }
                }
                catch (DocumentNonTrouveException ex)
                {
                    Console.WriteLine("❗ Erreur : " + ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("❗ Erreur de format : " + ex.Message);
                }
                catch (Exception ex)
                {
                    // Sécurité : ne pas faire planter le programme
                    Console.WriteLine("❗ Erreur inattendue : " + ex.Message);
                }
                finally
                {
                    Console.WriteLine("\n---- Fin de l'action ----\n");
                }
            }
        }

        // Méthode pour créer un document à partir des infos saisies au clavier
        static void AjouterDocumentDepuisConsole(Bibliotheque biblio)
        {
            Console.WriteLine("Type de document :");
            Console.WriteLine("1. Livre");
            Console.WriteLine("2. Magazine");
            Console.WriteLine("3. Document PDF");
            Console.Write("Votre choix : ");
            string? choixType = Console.ReadLine();

            Console.Write("Titre : ");
            string titre = Console.ReadLine() ?? "";

            Console.Write("Auteur : ");
            string auteur = Console.ReadLine() ?? "";

            Console.Write("Année : ");
            if (!int.TryParse(Console.ReadLine(), out int annee))
            {
                Console.WriteLine("Année invalide.");
                return;
            }

            Document? doc = null;

            switch (choixType)
            {
                case "1":
                    Console.Write("Nombre de pages : ");
                    if (!int.TryParse(Console.ReadLine(), out int pages))
                    {
                        Console.WriteLine("Nombre de pages invalide.");
                        return;
                    }
                    doc = new Livre(titre, auteur, annee, pages);
                    break;

                case "2":
                    Console.Write("Numéro du magazine : ");
                    if (!int.TryParse(Console.ReadLine(), out int numero))
                    {
                        Console.WriteLine("Numéro invalide.");
                        return;
                    }
                    doc = new Magazine(titre, auteur, annee, numero);
                    break;

                case "3":
                    Console.Write("Taille du PDF en Mo : ");
                    if (!double.TryParse(Console.ReadLine(), out double taille))
                    {
                        Console.WriteLine("Taille invalide.");
                        return;
                    }
                    doc = new DocumentPDF(titre, auteur, annee, taille);
                    break;

                default:
                    Console.WriteLine("Type invalide, annulation.");
                    return;
            }

            biblio.AjouterDocument(doc);
        }
    }
}
