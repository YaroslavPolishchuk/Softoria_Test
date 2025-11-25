using Softoria_Test.NewsScraper.Core.Models;

namespace Softoria_Test.NewsScraper.Core.Abstractions
{
    public abstract class NewsScraperBase : INewsScrapper
    {
        protected readonly INewsScraperEngine _engine;
        protected readonly string _url;
        public abstract string SourceName { get; }       
        protected NewsScraperBase(INewsScraperEngine engine, string url)
        {
            _engine = engine;
            _url = url;
        }
        public abstract Task<IEnumerable<NewsItem>> ScrapeAsync();
    }
}
