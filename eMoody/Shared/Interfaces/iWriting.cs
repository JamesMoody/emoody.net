using eMoody.Shared.Models;

namespace eMoody.Shared.Interfaces
{
    public interface iWriting : IDisposable
    {
        Task<IEnumerable<Article>> listArticlesAsync();
        Task<Article> getArticleAsync(string ArticleId);
    }
}