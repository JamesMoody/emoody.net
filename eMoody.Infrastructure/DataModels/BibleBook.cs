using System;
using System.Collections.Generic;
using System.Text;

namespace eMoody.Infrastructure.DataModels
{
    public class BibleBook
    {
        public int    BookId      { get; set; }
        public string Name        { get; set; }
        public string Testament   { get; set; }
        public int    GenreId     { get; set; }
        public int    numChapters { get; set; }
    }
}
