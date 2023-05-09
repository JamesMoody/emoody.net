using eMoody.Infrastructure;
using eMoody.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMoody.DAO
{
    public class Writing_InMemory : iWriting
    {
        #region locals and such...

        private Article[] Articles = {
            WritingArticles.createAPoem(),
            WritingArticles.createExcitement()
        };

        #endregion
        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                    // NOTE: NoOp... here per standard
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below; TODO: set large fields to null.
                // NOTE: NoOp... no unmanaged resources used

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Writing()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion

        #region iWriting - listArticlesAsync 

        public Task<IEnumerable<Article>> listArticlesAsync() {
            return Task.Run<IEnumerable<Article>>(() => { // we don't actually have an io wait here, so start a task and let everything async nicely
                return Articles;
            });
        }

        #endregion
        #region iWriting - getArticleAsync 

        public Task<Article> getArticleAsync(string ArticleId) {
            return Task.Run<Article>(() => { // we don't actually have an io wait here, so start a task and let everything async nicely
                return Articles.Where(p => p.ArticleId == ArticleId)
                               .FirstOrDefault();
            });
        }

        #endregion

    }
}
