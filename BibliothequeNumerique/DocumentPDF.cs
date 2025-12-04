using System;

namespace BibliothequeNumerique
{
    class DocumentPDF : Document
    {
        public double TailleEnMo { get; set; }

        public DocumentPDF(string titre, string auteur, int annee, double tailleEnMo)
            : base(titre, auteur, annee)
        {
            TailleEnMo = tailleEnMo;
        }

        public override void AfficherDetails()
        {
            Console.WriteLine($"[PDF] {Titre} - {Auteur} ({Annee}) | Taille : {TailleEnMo} Mo | Id : {Id}");
        }
    }
}
