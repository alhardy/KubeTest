using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace KubeTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureDefaultAppConfiguration(args)
                .UseStartup<Startup>();
    }
}