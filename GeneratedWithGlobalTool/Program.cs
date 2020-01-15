using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeneratedWithGlobalTool
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new BlogApiClient(new HttpClient());
            Console.ReadKey();
            var newArticle = new Article { Title = "Title", Author = "Author", Content = "Content"};
            await client.CreateArticleAsync(newArticle);
            Console.ReadKey();
            var articles = await client.GetArticlesAsync();
            foreach (var article in articles)
            {
                Console.WriteLine($"Article '{article.Title}' by {article.Author}");
            }
        }
    }
}