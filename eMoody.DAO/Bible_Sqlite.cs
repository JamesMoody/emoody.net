using Dapper;
using eMoody.Infrastructure;
using eMoody.Infrastructure.Configs;
using eMoody.Infrastructure.DataModels;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

// todo: this doesn't feel like the right place for this. Consider relocating later. 
namespace eMoody.DAO
{
    public class Bible_Sqlite : iBible
    {
        #region locals and such

        private SQLiteConnection _conn   = null;
        private DataConfig       _config = null;

        #endregion

        #region iBible - ListBooks

        private const string sql_ListBooks = "select b.b as BookId, b.n as Name, b.t as Testament, b.g as GenreId, max(k.c) as numChapters from key_english b inner join t_kjv k on b.b = k.b group by b.b, b.n, b.t, b.g;";
        public IEnumerable<BibleBook> ListBooks() { 
            return _conn.Query<BibleBook>(sql_ListBooks);
        }

        #endregion
        #region iBible - ListGenres

        private const string sql_ListGenres = "select g as GenreId, n as Name from key_genre_english order by g;";
        public IEnumerable<BibleGenre> ListGenres() {
            return _conn.Query<BibleGenre>(sql_ListGenres);
        }

        #endregion
        #region iBible - GetVerses

        private const string sql_GetVerses = "select id, b as BookId, c as Chapter, v as Verse, t as Text from t_kjv where b = @BookId and c = @Chapter order by v asc;";
        public IEnumerable<BibleVerse> GetVerses(int BookId, int Chapter) {
            var queryParams = new {
                BookId  = BookId, 
                Chapter = Chapter
            };
            return _conn.Query<BibleVerse>(sql_GetVerses, queryParams);
        }

        #endregion
        
        #region init

        public Bible_Sqlite(DataConfig diConfig) {
            _config = diConfig;
            _conn = new SQLiteConnection(_config.BibleConnString);
            _conn.Open();
        }

        #endregion

        #region IDisposable Support

        public void Dispose()
        {
            _conn.Dispose();
        }

        #endregion
    }
}
