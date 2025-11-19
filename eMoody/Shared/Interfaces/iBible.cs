using eMoody.Shared.Models;

namespace eMoody.Shared.Interfaces
{
    public interface iBible : IDisposable
    {
        Task<IEnumerable<BibleBook>>  ListBooksAsync();
        Task<IEnumerable<BibleGenre>> ListGenresAsync();
        Task<IEnumerable<BibleVerse>> GetVersesAsync(int BookId, int Chapter);
    }
}