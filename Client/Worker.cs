using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Client
{
    public class Worker : BackgroundService
    {
        private readonly IHttpClientFactory _factory;
        private const string CreateCommand = "create";
        private const string GetListCommand = "get-list";

        public Worker(IHttpClientFactory factory)
        {
            _factory = factory;
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
                
                var client = _factory.CreateClient("localhost");
                if (command is GetListCommand)
                {
                    using var response = await client.GetAsync("api/article/list", stoppingToken);
                    var content = await response.Content.ReadAsStringAsync();
                    var articles = JsonSerializer.Deserialize<List<Article>>(content, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
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
                    var jsonArticle = JsonSerializer.Serialize(article);
                    using var jsonContent = new StringContent(jsonArticle, Encoding.UTF8, "application/json");
                    await client.PostAsync("api/article/create", jsonContent, stoppingToken);
                }
            }
        }
    }
}
