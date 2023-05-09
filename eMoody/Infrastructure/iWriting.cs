using eMoody.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMoody.Infrastructure
{
    public interface iWriting : IDisposable
    {
        Task<IEnumerable<Article>> listArticlesAsync();
        Task<Article> getArticleAsync(string ArticleId);

    }
}
