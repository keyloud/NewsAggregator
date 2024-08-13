using HtmlAgilityPack;
using NewsAggregator.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public class HtmlParser : IArticleParser
{
    public async Task<List<NewsArticle>> ParseAsync(string htmlContent)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(htmlContent);

        var articles = new List<NewsArticle>();

         // XPath для поиска элементов <a> с классом, содержащим заголовки и URL
        var nodes = doc.DocumentNode.SelectNodes("//a[@class='_0QcT8 sNYHq']");

        if (nodes != null)
        {
            foreach (var node in nodes)
            {
                // Извлечение заголовка
                var titleNode = node.SelectSingleNode(".//div[@class='p1Gbz']");
                var title = titleNode?.InnerText.Trim() ?? "No Title";
                
                // Извлечение URL
                var url = node.GetAttributeValue("href", string.Empty);
                // Формирование полного URL, если необходимо
                var fullUrl = $"https://www.culture.ru{url}";

                var news = new NewsArticle
                {
                    Title = title,
                    Url = fullUrl, // Если есть URL, добавьте логику для его извлечения
                    PublishedDate = DateTime.Now // Добавьте логику для извлечения даты, если доступна
                };

                articles.Add(news);
            }
        }
        else
        {
            Console.WriteLine("Не удалось найти заголовки новостей.");
        }

        return articles;
    }
}
