using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KubeTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                var configuration = context.RequestServices.GetRequiredService<IConfiguration>();
                var secretKey = configuration.GetValue<string>("SecretKey");

                if (context.Request.Path == "/up")
                {
                    if (DateTime.UtcNow.Minute % 2 == 0)
                    {
                        await context.Response.WriteAsync("up");
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status404NotFound;
                        await context.Response.WriteAsync("down");
                    }
                }
                else
                {
                    await context.Response.WriteAsync($"Hello World from {Environment.MachineName}. Secret Key: {secretKey}");
                }
            });
        }
    }
}