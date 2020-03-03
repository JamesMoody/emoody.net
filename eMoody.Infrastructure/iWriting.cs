using eMoody.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace eMoody.Infrastructure
{
    public interface iWriting : IDisposable
    {
        IEnumerable<Article> listArticles();
        Article getArticle(string ArticleId);

    }
}
