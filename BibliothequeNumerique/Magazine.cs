using System;

namespace BibliothequeNumerique
{
    class Magazine : Document
    {
        public int Numero { get; set; }

        public Magazine(string titre, string auteur, int annee, int numero)
            : base(titre, auteur, annee)
        {
            Numero = numero;
        }

        public override void AfficherDetails()
        {
            Console.WriteLine($"[Magazine] {Titre} - {Auteur} ({Annee}) | Numéro : {Numero} | Id : {Id}");
        }
    }
}
