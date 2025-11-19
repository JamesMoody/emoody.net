using eMoody.Shared.Interfaces;
using eMoody.Shared.Models;
using System.Net.Http.Json;

namespace eMoody.Client.DAOFacade
{
    public class WritingFacade : iWriting
    {

        #region locals and such

        private HttpClient http = null;

        #endregion
        #region init

        public WritingFacade(HttpClient httpDI) {
            http = httpDI;
        }

        #endregion
        #region Dispose

        public void Dispose() {
            // no-op
        }

        #endregion

        #region iWriting data facades

        public       Task<Article>              getArticleAsync(string ArticleId) => throw new NotImplementedException();
        public async Task<IEnumerable<Article>> listArticlesAsync()               => await http.GetFromJsonAsync<IEnumerable<Article>>("api/article");

        #endregion

    }
}
