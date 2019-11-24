using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Client
{
    public class WorkerWithRefit : BackgroundService
    {
        private readonly IArticlesApi _articlesApi;
        private const string CreateCommand = "create";
        private const string GetListCommand = "get-list";

        public WorkerWithRefit(IArticlesApi articlesApi)
        {
            _articlesApi = articlesApi;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine("Type a command.");
                var command = Console.ReadLine();
                if (command != CreateCommand && command != GetListCommand)
                {
                    Console.WriteLine("Wrong command. Try again.");
                    continue;
                }

                if (command is GetListCommand)
                {
                    var articles = await _articlesApi.GetArticlesList();
                    Console.WriteLine($"We have {articles.Count} articles");
                    foreach (var article in articles)
                    {
                        Console.WriteLine($"Article '{article.Title}' by {article.Author}");
                    }
                } else if (command is CreateCommand)
                {
                    Console.WriteLine("Type a title");
                    var title = Console.ReadLine();
                    Console.WriteLine("Type an author");
                    var author = Console.ReadLine();
                    Console.WriteLine("Type a content");
                    var content = Console.ReadLine();
                    
                    var article = new Article {Title = title, Author = author, Content = content};
                    await _articlesApi.CreateArticle(article);
                }
            }
        }
    }
}