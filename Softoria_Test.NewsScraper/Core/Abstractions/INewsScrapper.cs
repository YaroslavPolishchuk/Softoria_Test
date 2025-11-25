using Softoria_Test.NewsScraper.Core.Models;

namespace Softoria_Test.NewsScraper.Core.Abstractions
{
    public interface INewsScrapper
    {
        string SourceName { get; }
        Task<IEnumerable<NewsItem>> ScrapeAsync();
    }
}
