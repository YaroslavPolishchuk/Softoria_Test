using Microsoft.Playwright;
using Softoria_Test.NewsScraper.Core.Abstractions;

namespace Softoria_Test.NewsScraper.Infrastructure.Engines
{
    public class PlaywrightEngine : INewsScraperEngine
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IPage _page;

        public async ValueTask DisposeAsync()
        {
            if (_page != null) await _page.CloseAsync();
            if (_browser != null) await _browser.CloseAsync();
            _playwright?.Dispose();
        }

        public async Task Initialize()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Firefox.LaunchAsync(new()
            {
                Headless = false                
            });
            var context = await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize { Width = 1920, Height = 1200 }
            });
            _page = await _browser.NewPageAsync();
        }

        public async Task NavigateTo(string url)
        {
            await _page.GotoAsync(url);
            await Task.Delay(1000);
        }

        public async Task ScrollTo()
        {
            await _page.Keyboard.PressAsync("End");
            await Task.Delay(500);
        }

        public async Task<IReadOnlyCollection<ILocator>> Element(string selector)
        {
           return await _page.Locator(selector).AllAsync();
        }

        public async Task<string> GetAttribute(ILocator element, string attributeName)
        {
            return await element.GetAttributeAsync(attributeName);
        }

        public async Task WaitForLoading(string selector)
        {
            var locator = _page.Locator(selector);
            if(await locator.IsVisibleAsync())
            {
                await locator.WaitForAsync(new LocatorWaitForOptions
                {
                    State = WaitForSelectorState.Hidden, 
                    Timeout = 30000
                });
            }
        }
    }
}
