using Dapper;
using eMoody.Data.Configuration;
using eMoody.Shared.Interfaces;
using eMoody.Shared.Models;
using System.Data.SQLite;

namespace eMoody.Data.Implementations
{
    public class Bible_Sqlite : iBible
    {
        #region locals and such

        private SQLiteConnection _conn = null;
        private BibleConfig _dbConfig = null;

        #endregion
        #region init

        public Bible_Sqlite(BibleConfig dbConfigDI) {
            _dbConfig = dbConfigDI;
            _conn = new SQLiteConnection(_dbConfig.BibleConnString);
            _conn.Open();
        }

        #endregion
        #region IDisposable Support

        public void Dispose() {
            _conn.Dispose();
        }

        #endregion

        #region iBible - ListBooksAsync

        private const string sql_ListBooks = "select b.b as BookId, b.n as Name, b.t as Testament, b.g as GenreId, max(k.c) as numChapters from key_english b inner join t_kjv k on b.b = k.b group by b.b, b.n, b.t, b.g;";
        public async Task<IEnumerable<BibleBook>> ListBooksAsync() {
            return await _conn.QueryAsync<BibleBook>(sql_ListBooks);
        }

        #endregion
        #region iBible - ListGenresAsync

        private const string sql_ListGenres = "select g as GenreId, n as Name from key_genre_english order by g;";
        public async Task<IEnumerable<BibleGenre>> ListGenresAsync() {
            return await _conn.QueryAsync<BibleGenre>(sql_ListGenres);
        }

        #endregion
        #region iBible - GetVersesAsync

        private const string sql_GetVerses = "select id, b as BookId, c as Chapter, v as Verse, t as Text from t_kjv where b = @BookId and c = @Chapter order by v asc;";
        public async Task<IEnumerable<BibleVerse>> GetVersesAsync(int BookId, int Chapter) {
            var queryParams = new {
                BookId = BookId,
                Chapter = Chapter
            };
            return await _conn.QueryAsync<BibleVerse>(sql_GetVerses, queryParams);
        }

        #endregion

    }
}