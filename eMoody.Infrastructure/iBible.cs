using eMoody.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace eMoody.Infrastructure
{
    public interface iBible : IDisposable
    {
        IEnumerable<BibleBook>  ListBooks();
        IEnumerable<BibleGenre> ListGenres();
        IEnumerable<BibleVerse> GetVerses(int BookId, int Chapter);
    }
}
