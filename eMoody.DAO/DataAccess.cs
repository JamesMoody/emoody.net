using eMoody.Infrastructure;
using eMoody.Infrastructure.Configs;
using System;

namespace eMoody.DAO
{
    public class DataAccess : iDataAccess
    {

        #region locals and such

        private DataConfig _config = null;

        #endregion

        #region init

        public DataAccess(DataConfig configDI) {
            _config = configDI;
        }

        #endregion

        #region databases

        public iWriting Writing { get => new Writing_InMemory(); }

        public iBible   Bible   { get => new Bible_Sqlite(_config); }

        #endregion

    }
}
