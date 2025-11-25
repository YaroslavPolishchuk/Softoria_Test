using Microsoft.Playwright;

namespace Softoria_Test.NewsScraper.Core.Abstractions
{
    public interface INewsScraperEngine : IAsyncDisposable
    {
        Task Initialize();
        Task NavigateTo(string url);
        Task ScrollTo();
        Task<IReadOnlyCollection<ILocator>> Element(string selector);
        Task WaitForLoading(string selector);
        Task<string> GetAttribute(ILocator element, string attributeName);
    }
}
