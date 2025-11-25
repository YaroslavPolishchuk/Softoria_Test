using Microsoft.EntityFrameworkCore;
using Softoria_Test.NewsScraper.Core.Models;
using Softoria_Test.NewsScraper.Infrastructure.Data;

namespace Softoria_Test.NewsScraper.Infrastructure.Repositories
{

    public class NewsRepository : INewsRepository
    {
        private readonly NewsDbContext _context;

        public NewsRepository(NewsDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewItems(IEnumerable<NewsItem> items)
        {
            //check if items ok
            var validItems = items
                .Where(x => !string.IsNullOrWhiteSpace(x.Title) &&
                           !string.IsNullOrWhiteSpace(x.Url))
                .ToList();
            if (!validItems.Any())
                return 0;
            //

            //check for duplicates
            var recent = GetRecent();
            var recentUrls = recent.Select(x => x.Url).ToHashSet();
            var recentTitles = recent.Select(x => x.Title).ToHashSet();

            var newItems=validItems.Where(x=>!recentTitles.Contains(x.Title) && !recentUrls.Contains(x.Url)).ToList();
            if (!newItems.Any())
                return 0;
            //

            //add
            newItems.Reverse();
            await _context.AddRangeAsync(newItems);
            await _context.SaveChangesAsync();

            return newItems.Count();
        }

        public List<NewsItem> GetRecent(int skipCount = 50)
        {
            return _context.NewsItems.OrderByDescending(x => x.Id).Take(skipCount).AsNoTracking().ToList();
        }
    }
}
