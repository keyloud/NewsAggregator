using System;
using System.IO;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static async Task Main(string[] args)
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless"); // Запуск в фоновом режиме
        using var driver = new ChromeDriver(options);

        driver.Navigate().GoToUrl("https://www.culture.ru/cinema/articles");

        // Подождите, пока элементы загрузятся, если нужно
        await Task.Delay(5000); // Задержка для загрузки данных

        // Получение HTML-кода страницы
        var htmlContent = driver.PageSource;

        // Используйте ваш парсер
        var parser = new HtmlParser();
        var articles = await parser.ParseAsync(htmlContent);

        Console.WriteLine($"Fetched {articles.Count} articles.");

        foreach (var article in articles)
        {
            Console.WriteLine($"Title: {article.Title}");
            Console.WriteLine($"URL: {article.Url}");
            Console.WriteLine($"Published Date: {article.PublishedDate}");
        }

        // Создайте экземпляр NewsService и сохраните новости в базу данных
        var newsService = new NewsService(parser);
        await newsService.SaveNewsAsync(articles);


        // Теперь извлекаем данные из базы данных и выводим их на консоль
        using (var db = new NewsContext())
        {
            var savedArticles = db.NewsArticles.ToList();
            Console.WriteLine($"Total articles in database: {savedArticles.Count}");

            foreach (var savedArticle in savedArticles)
            {
                Console.WriteLine($"Title: {savedArticle.Title}");
                Console.WriteLine($"URL: {savedArticle.Url}");
                Console.WriteLine($"Published Date: {savedArticle.PublishedDate}");
                Console.WriteLine();
            }
        }
    }
}
