using eMoody.Shared.Interfaces;

namespace eMoody.Client.DAOFacade
{
    public class DataAccess : iDataAccess
    {
        #region locals and such

        private HttpClient http = null;

        #endregion
        #region init

        public DataAccess(HttpClient httpDI) {
            http = httpDI;
        }

        #endregion

        #region iDataAccess - Writing & Bible facades

        public iWriting Writing => new WritingFacade(http);

        public iBible   Bible   => new BibleFacade(http);

        #endregion

    }
}
