using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eMoody.Infrastructure;
using eMoody.Infrastructure.DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eMoody.Server.Features.KJV
{
    [Route("api/bible")]
    [ApiController]
    public class BibleController : ControllerBase
    {

        #region locals and such

        private ILogger<BibleController> logger = null;
        private iDataAccess              dao    = null;

        #endregion
        #region init

        public BibleController(ILogger<BibleController> loggerDI, iDataAccess daoDI) {
            logger = loggerDI;
            dao    = daoDI;
        }

        #endregion

        #region api - "/api/bible/kjv/{BookId}/{Chapter}"

        [HttpGet()]
        [Route("kjv/{BookId:int}/{Chapter:int}")]
        public async Task<IEnumerable<BibleVerse>> GETChapter(int BookId, int Chapter) {
            IEnumerable<BibleVerse> ret = null;

            using (var db = dao.Bible) {
                ret = await db.GetVersesAsync(BookId, Chapter);
            }

            return ret;
        }

        #endregion
        #region api - "/api/bible/kjv/Books"

        [HttpGet()]
        [Route("kjv/books")]
        public async Task<IEnumerable<BibleBook>> GETBooks() {
            IEnumerable<BibleBook> ret = null;

            using (var db = dao.Bible) {
                ret = await db.ListBooksAsync();
            }

            return ret;
        }

        #endregion
        #region api - "/api/bible/kjv/Genres"

        [HttpGet()]
        [Route("kjv/genres")]
        public async Task<IEnumerable<BibleGenre>> GETGenres() {
            IEnumerable<BibleGenre> ret = null;

            using (var db = dao.Bible) {
                ret = await db.ListGenresAsync();
            }

            return ret;
        }

        #endregion


    }
}
