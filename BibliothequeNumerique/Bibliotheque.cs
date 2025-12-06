using System;
using System.Collections.Generic;
using System.IO;

namespace BibliothequeNumerique
{
    class Bibliotheque
    {
        // Liste de tous les documents (livres, magazines, pdf)
        private List<Document> documents = new List<Document>();

        // 1) Ajouter un document
        public void AjouterDocument(Document d)
        {
            documents.Add(d);
            Console.WriteLine(" Document ajouté avec succès.");
        }

        // 2) Supprimer un document par Id
        public void SupprimerDocument(Guid id)
        {
            Document doc = documents.Find(d => d.Id == id);

            if (doc == null)
            {
                // Si pas trouvé, on lance notre exception personnalisée
                throw new DocumentNonTrouveException("Document introuvable pour l'Id : " + id);
            }

            documents.Remove(doc);
            Console.WriteLine(" Document supprimé avec succès.");
        }

        // 3) Rechercher par mot-clé (dans titre ou auteur)
        public List<Document> Rechercher(string motCle)
        {
            motCle = motCle.ToLower();

            var resultats = documents.FindAll(d =>
                d.Titre.ToLower().Contains(motCle) ||
                d.Auteur.ToLower().Contains(motCle));

            if (resultats.Count == 0)
            {
                throw new DocumentNonTrouveException("Aucun document trouvé pour : " + motCle);
            }

            return resultats;
        }

        // 4) Afficher tous les documents
        public void AfficherTous()
        {
            if (documents.Count == 0)
            {
                Console.WriteLine(" Aucun document dans la bibliothèque.");
                return;
            }

            Console.WriteLine(" Liste des documents :");
            foreach (var d in documents)
            {
                d.AfficherDetails(); // polymorphisme : appelle la bonne version selon le type
            }
        }

       //////////////////////////////////////

        public void Sauvegarder(string cheminFichier)
        {
            try
            {
                // using => ferme automatiquement le fichier après
                using (FileStream fs = new FileStream(cheminFichier, FileMode.Create))
                using (StreamWriter writer = new StreamWriter(fs))
                {
                    foreach (var d in documents)
                    {
                        string type = "";
                        string extra = "";

                        if (d is Livre livre)
                        {
                            type = "LIVRE";
                            extra = livre.NombrePages.ToString();
                        }
                        else if (d is Magazine mag)
                        {
                            type = "MAGAZINE";
                            extra = mag.Numero.ToString();
                        }
                        else if (d is DocumentPDF pdf)
                        {
                            type = "PDF";
                            extra = pdf.TailleEnMo.ToString();
                        }

                        string ligne = $"{type};{d.Id};{d.Titre};{d.Auteur};{d.Annee};{extra}";
                        writer.WriteLine(ligne);
                    }
                }

                Console.WriteLine("Sauvegarde réussie dans : " + cheminFichier);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Erreur d'accès au fichier : " + ex.Message);
            }
        }

        public void Charger(string cheminFichier)
        {
            try
            {
                if (!File.Exists(cheminFichier))
                {
                    Console.WriteLine(" Fichier introuvable.");
                    return;
                }

                documents.Clear(); // on vide l'ancienne liste

                using (FileStream fs = new FileStream(cheminFichier, FileMode.Open))
                using (StreamReader reader = new StreamReader(fs))
                {
                    string? ligne;
                    while ((ligne = reader.ReadLine()) != null)
                    {
                        // Chaque ligne : type;id;titre;auteur;annee;extra
                        string[] parts = ligne.Split(';');

                        if (parts.Length != 6)
                        {
                            Console.WriteLine(" Ligne incorrecte : " + ligne);
                            continue;
                        }

                        string type = parts[0];
                        Guid id = Guid.Parse(parts[1]);
                        string titre = parts[2];
                        string auteur = parts[3];
                        int annee = int.Parse(parts[4]);
                        string extra = parts[5];

                        Document? doc = null;

                        switch (type)
                        {
                            case "LIVRE":
                                doc = new Livre(titre, auteur, annee, int.Parse(extra));
                                break;

                            case "MAGAZINE":
                                doc = new Magazine(titre, auteur, annee, int.Parse(extra));
                                break;

                            case "PDF":
                                doc = new DocumentPDF(titre, auteur, annee, double.Parse(extra));
                                break;

                            default:
                                Console.WriteLine(" Type inconnu : " + type);
                                break;
                        }

                        if (doc != null)
                        {
                            // on remet le vrai Id du fichier
                            doc.Id = id;
                            documents.Add(doc);
                        }
                    }
                }

                Console.WriteLine(" Chargement terminé depuis : " + cheminFichier);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Erreur d'accès au fichier : " + ex.Message);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Erreur de format de fichier : " + ex.Message);
            }
        }
    }
}
