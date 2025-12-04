using System;

namespace BibliothequeNumerique
{
    // Classe abstraite => elle sert de base, on ne peut pas créer Document directement
    abstract class Document
    {
        public Guid Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public int Annee { get; set; }

        // Constructeur de base
        protected Document(string titre, string auteur, int annee)
        {
            Id = Guid.NewGuid();  // Génère un Id unique
            Titre = titre;
            Auteur = auteur;
            Annee = annee;
        }

        // Méthode abstraite => les classes enfants devront l'implémenter
        public abstract void AfficherDetails();
    }
}
