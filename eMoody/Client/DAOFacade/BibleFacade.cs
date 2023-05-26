using eMoody.Infrastructure;
using eMoody.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace eMoody.Client.DAOFacade
{
    public class BibleFacade : iBible
    {
        #region locals and such

        private HttpClient http = null;

        #endregion
        #region init

        public BibleFacade(HttpClient httpDI) {
            http = httpDI;
        }

        #endregion
        #region Dispose

        public void Dispose() {
            // no-op
        }

        #endregion

        #region iBible data facades

        public async Task<IEnumerable<BibleVerse>> GetVersesAsync(int BookId, int Chapter) => await http.GetFromJsonAsync<IEnumerable<BibleVerse>>($"api/bible/kjv/{BookId}/{Chapter}");
        public async Task<IEnumerable<BibleBook>>  ListBooksAsync()                        => await http.GetFromJsonAsync<IEnumerable<BibleBook>> ("api/bible/kjv/books");
        public async Task<IEnumerable<BibleGenre>> ListGenresAsync()                       => await http.GetFromJsonAsync<IEnumerable<BibleGenre>>("api/bible/kjv/genres");

        #endregion
    }
}
