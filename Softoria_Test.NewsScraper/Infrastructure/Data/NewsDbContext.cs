using Microsoft.EntityFrameworkCore;
using Softoria_Test.NewsScraper.Core.Models;

namespace Softoria_Test.NewsScraper.Infrastructure.Data
{
    public class NewsDbContext : DbContext
    {
        public DbSet<NewsItem> NewsItems { get; set; }
        public NewsDbContext(DbContextOptions<NewsDbContext> options)
            : base(options)
        {
        }
        public NewsDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                    "Host=localhost;Port=5432;Database=news;Username=admin;Password=secret"
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsItem>(entity =>
            {
                entity.ToTable("newsitems");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(500)
                    .IsRequired();

                entity.Property(e => e.Url)
                    .HasColumnName("url")
                    .HasMaxLength(1000)
                    .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .HasColumnName("createdat")
                    .HasColumnType("timestamp with time zone");

                entity.HasIndex(e => e.Title)
                    .HasDatabaseName("idx_news_items_title");
            });
        }
    }
}
