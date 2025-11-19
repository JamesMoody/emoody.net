using eMoody.Shared.Interfaces;
using eMoody.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace eMoody.Server.Features.Writing
{
    [Route("api/article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {

        #region locals and such

        private ILogger<ArticleController> logger = null;
        private iDataAccess                dao    = null;

        #endregion
        #region init

        public ArticleController(ILogger<ArticleController> loggerDI, iDataAccess daoDI) {
            logger = loggerDI;
            dao    = daoDI;
        }

        #endregion

        #region api - get (list)

        [HttpGet]
        public async Task<IEnumerable<Article>> GET() {
            IEnumerable<Article> ret = null;

            using (var db = dao.Writing) {
                ret = await db.listArticlesAsync();
            }

            return ret;
        }

        #endregion

    }
}
