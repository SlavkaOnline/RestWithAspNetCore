using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    //services.AddHostedService<Worker>();
                    services.AddHttpClient("localhost", c =>
                    {
                        c.BaseAddress = new Uri("http://localhost:5000");
                        c.DefaultRequestHeaders.Add("Accept", "application/json");
                    });
                    
                    services.AddHostedService<WorkerWithRefit>();
                    services.AddRefitClient<IArticlesApi>()
                        .ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5000"));
                });
    }
}
