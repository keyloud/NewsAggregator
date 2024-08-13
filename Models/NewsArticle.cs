
namespace NewsAggregator.Models
{
    public class NewsArticle
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public string Url { get; set; }
        public DateTime PublishedDate { get; set; }
    }

}
