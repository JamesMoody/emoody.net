using eMoody.Data.Configuration;
using eMoody.Shared.Interfaces;

namespace eMoody.Data.Implementations
{
    public class DataAccess : iDataAccess
    {

        #region locals and such

        private BibleConfig _bibleConfig = null;

        #endregion
        #region init

        public DataAccess(BibleConfig bibleConfigDI) {
            _bibleConfig = bibleConfigDI;
        }

        #endregion

        #region databases

        public iWriting Writing { get => new Writing_InMemory(); }

        public iBible Bible { get => new Bible_Sqlite(_bibleConfig); }

        #endregion

    }
}