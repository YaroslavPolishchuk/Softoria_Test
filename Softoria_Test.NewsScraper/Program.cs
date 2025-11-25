using Microsoft.Extensions.DependencyInjection;
using Softoria_Test.NewsScraper.Core.Abstractions;
using Softoria_Test.NewsScraper.Infrastructure.Data;
using Softoria_Test.NewsScraper.Infrastructure.Engines;
using Softoria_Test.NewsScraper.Infrastructure.Scrapers;
using Microsoft.EntityFrameworkCore;
using Softoria_Test.NewsScraper.Infrastructure.Repositories;

var services = new ServiceCollection();
services.AddDbContext<NewsDbContext>(opt => opt.UseNpgsql(
    "Host=localhost;Port=5432;Database=news;Username=admin;Password=secret"
    ));
services.AddScoped<INewsRepository, NewsRepository>();
var serviceProvider = services.BuildServiceProvider();
INewsRepository newsRepository = serviceProvider.GetRequiredService<INewsRepository>();

INewsScraperEngine engine = new PlaywrightEngine();
INewsScrapper scraper = new YahooFinanceScraper(engine,newsRepository.GetRecent());


var scrapResult=await scraper.ScrapeAsync();

var addResult=await newsRepository.AddNewItems(scrapResult);



