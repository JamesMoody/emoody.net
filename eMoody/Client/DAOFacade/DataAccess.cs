﻿using eMoody.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
