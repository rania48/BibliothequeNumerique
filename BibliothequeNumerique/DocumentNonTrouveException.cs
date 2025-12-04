using System;

namespace BibliothequeNumerique
{
    // Exception personnalisée pour notre bibliothèque
    class DocumentNonTrouveException : Exception
    {
        public DocumentNonTrouveException(string message) : base(message)
        {
        }
    }
}
//rq on utilisera cette exception quand on cherche ou supprime un document qui n’existe pas