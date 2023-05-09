using eMoody.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMoody.Infrastructure
{
    public interface iBible : IDisposable
    {
        Task<IEnumerable<BibleBook>>  ListBooksAsync();
        Task<IEnumerable<BibleGenre>> ListGenresAsync();
        Task<IEnumerable<BibleVerse>> GetVersesAsync(int BookId, int Chapter);
    }
}