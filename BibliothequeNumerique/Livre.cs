using System;

namespace BibliothequeNumerique
{
    // Livre hérite de Document
    class Livre : Document
    {
        public int NombrePages { get; set; }

        public Livre(string titre, string auteur, int annee, int nombrePages)
            : base(titre, auteur, annee)   // appelle le constructeur de Document
        {
            NombrePages = nombrePages;
        }

        // On doit obligatoirement implémenter AfficherDetails()
        public override void AfficherDetails()
        {
            Console.WriteLine($"[Livre] {Titre} - {Auteur} ({Annee}) | Pages : {NombrePages} | Id : {Id}");
        }
    }
}
