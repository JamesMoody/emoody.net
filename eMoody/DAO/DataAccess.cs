using eMoody.Config;
using eMoody.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eMoody.DAO
{
    public class DataAccess : iDataAccess
    {

        #region locals and such

        // private DataConfig _config = null;
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
