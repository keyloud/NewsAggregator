using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using NewsAggregator.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class NewsService
{
    private readonly HttpClient _httpClient;
    private readonly IArticleParser _parser;

    public NewsService(IArticleParser parser)
    {
        _httpClient = new HttpClient();
        _parser = parser;
    }

    // Метод для получения и парсинга новостей с указанного URL
    public async Task<List<NewsArticle>> FetchAndParseAsync(string url)
    {
        var htmlContent = await _httpClient.GetStringAsync(url);
        if (string.IsNullOrEmpty(htmlContent))
        {
            Console.WriteLine("Ошибка: не удалось загрузить HTML-контент");
        }
        return await _parser.ParseAsync(htmlContent);
    }

    // Метод для сохранения списка новостей в базу данных
    public async Task SaveNewsAsync(List<NewsArticle> articles)
    {
        try
        {
            using (var db = new NewsContext())
            {
                db.Database.EnsureCreated();

                foreach (var article in articles)
                {
                    // Проверяем, существует ли статья с таким же URL
                    bool articleExists = db.NewsArticles.Any(a => a.Url == article.Url);

                    if (!articleExists)
                    {
                        db.NewsArticles.Add(article);
                    }
                }

                await db.SaveChangesAsync();
                Console.WriteLine("Articles saved to the database.");
            }
        }
        catch (DbUpdateException dbEx)
        {
            Console.WriteLine($"Database update error: {dbEx.Message}");
            Console.WriteLine($"Inner exception: {dbEx.InnerException?.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
