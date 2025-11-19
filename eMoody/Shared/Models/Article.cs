
namespace eMoody.Shared.Models
{
    public class Article
    {
        public string   ArticleId { get; set; }
        public string   Title     { get; set; }
        public string   Content   { get; set; }
        public string   Author    { get; set; }
        public DateTime dCreated  { get; set; }
    }
}