using Microsoft.EntityFrameworkCore;
using NewsAggregator.Models;

public class NewsContext : DbContext
{
    public DbSet<NewsArticle> NewsArticles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:/Users/kosol/Desktop/future/NewsAggregator/news.db");
    }
}
