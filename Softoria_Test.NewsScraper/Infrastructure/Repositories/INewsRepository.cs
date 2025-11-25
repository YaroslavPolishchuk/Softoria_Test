using Softoria_Test.NewsScraper.Core.Models;

namespace Softoria_Test.NewsScraper.Infrastructure.Repositories
{
    public interface INewsRepository
    {
        List<NewsItem> GetRecent(int skipCount=50);
        Task<int> AddNewItems(IEnumerable<NewsItem> items);
    }
}
