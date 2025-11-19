
namespace eMoody.Shared.Models
{
    public class BibleVerse
    {
        public int    Id      { get; set; }
        public int    BookId  { get; set; }
        public int    Chapter { get; set; }
        public int    Verse   { get; set; }
        public string Text    { get; set; }
    }
}