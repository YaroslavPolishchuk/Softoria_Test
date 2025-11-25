using Microsoft.Playwright;
using Softoria_Test.NewsScraper.Core.Abstractions;
using Softoria_Test.NewsScraper.Core.Models;

namespace Softoria_Test.NewsScraper.Infrastructure.Scrapers
{
    public class YahooFinanceScraper : NewsScraperBase
    {
        public override string SourceName => "Yahoo Finance";

        private const string URL = "https://finance.yahoo.com/topic/latest-news/";
        private const string NEWS_ITEM_SELECTOR = "//li[contains(@class,'stream-item')]/section[@role='article']/a";
        private const string TITLE_SELECTOR = "title";
        private const string LINK_SELECTOR = "href";
        private const string LOAD_SELECTOR = "//*[@aria-label='Progress Spinner']";
        private const int initNews = 50;
        private List<NewsItem> recentNews;

        public YahooFinanceScraper(INewsScraperEngine engine, List<NewsItem> recentNews)
            : base(engine, URL)
        {
            this.recentNews = recentNews;
        }

        public async override Task<IEnumerable<NewsItem>> ScrapeAsync()
        {            
            var recentTitles = recentNews.Select(x => x.Title).ToHashSet();
            var newsItems = new List<NewsItem>();
            try
            {
                await _engine.Initialize();
            }
            catch (Exception ex) { throw; }
            
            await _engine.NavigateTo(URL);

            //Check if nedd to upload news
            var initialElements = await _engine.Element(NEWS_ITEM_SELECTOR);
            bool scroll = true;
            IEnumerable<ILocator> old = initialElements.Where(x => !recentTitles.Contains(_engine.GetAttribute(x, TITLE_SELECTOR).Result));
            var suak = old.Count();
            if (old.Count() == 0)
                return newsItems;

            if (old.Count() < initNews)
                scroll = false;
            //

            IReadOnlyCollection<ILocator> newsElements = Array.Empty<ILocator>();            
            
            //Scroll to end
            while (scroll)
            {                
                await _engine.ScrollTo();
                await _engine.WaitForLoading(LOAD_SELECTOR);
                newsElements = await _engine.Element(NEWS_ITEM_SELECTOR);

                if (initialElements.Last().GetAttributeAsync(TITLE_SELECTOR).Result == newsElements.Last().GetAttributeAsync(TITLE_SELECTOR).Result)
                    scroll = false;
                initialElements = newsElements;
            }
            //

            //Get collection of models from web elements
            foreach (var item in initialElements)
            {
                var title = await _engine.GetAttribute(item, TITLE_SELECTOR);
                var url = await _engine.GetAttribute(item, LINK_SELECTOR);

                var newsItem = new NewsItem();
                newsItem.Title = title;
                newsItem.Url = url;

                if (!string.IsNullOrWhiteSpace(title) && !string.IsNullOrWhiteSpace(url))
                {
                    newsItems.Add(new NewsItem
                    {
                        Title = title.Trim(),
                        Url = url,
                        CreatedAt = DateTime.UtcNow,
                    });
                }
            }
            return newsItems;
        }
    }
}
