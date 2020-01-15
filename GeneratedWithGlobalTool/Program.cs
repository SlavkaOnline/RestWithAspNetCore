using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GeneratedWithGlobalTool
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var client = new ContractClient(new HttpClient());
            Console.ReadKey();
            var body = new Body { Title = "Title", Author = "Author", Content = "Content"};
            await client.CreateAsync(body);
            Console.ReadKey();
            var articles = await client.ListAsync();
            foreach (var article in articles)
            {
                Console.WriteLine($"Article '{article.Title}' by {article.Author}");
            }
        }
    }
}