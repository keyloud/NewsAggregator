using System.Collections.Generic;
using System.Threading.Tasks;
using NewsAggregator.Models;

public interface IArticleParser
{
    Task<List<NewsArticle>> ParseAsync(string htmlContent);
}
