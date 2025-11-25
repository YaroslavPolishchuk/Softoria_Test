namespace Softoria_Test.NewsScraper.Core.Models
{
    public class NewsItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }      
        public DateTime CreatedAt { get; set; }
    }
}
