using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;

namespace KubeTest
{
    public static class ProgramConfiguration
    {
        public static IWebHostBuilder ConfigureDefaultAppConfiguration(this IWebHostBuilder webHostBuilder, string[] args)
        {
            webHostBuilder.ConfigureAppConfiguration(
                (context, config) =>
                {
                    config.AddDefaultSources(args);
                });

            return webHostBuilder;
        }

        public static IConfigurationBuilder AddDefaultSources(this IConfigurationBuilder builder, string[] args = null)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Constants.Environments.CurrentAspNetCoreEnv ?? Constants.Environments.Production}.json", optional: true)
                .AddEnvironmentVariables();

            if (Constants.Environments.IsDevelopment())
            {
                builder.AddUserSecrets(Assembly.GetExecutingAssembly());
                AddDefaultAzureKeyVaultUsingClientCredentials(builder);
            }
            else
            {
                AddDefaultAzureKeyVaultUsingMsi(builder);
            }

            if (args != null)
            {
                builder.AddCommandLine(args);
            }

            return builder;
        }

        private static void AddDefaultAzureKeyVaultUsingMsi(IConfigurationBuilder builder)
        {
            var builtConfig = builder.Build();
            var keyVaultName = builtConfig.GetValue<string>("keyVaultName");

            if (keyVaultName == null)
            {
                return;
            }

            var keyVaultEndpoint = $"https://{keyVaultName}.vault.azure.net/";
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            builder.AddAzureKeyVault(keyVaultEndpoint, keyVaultClient, new DefaultKeyVaultSecretManager());
        }

        private static void AddDefaultAzureKeyVaultUsingClientCredentials(IConfigurationBuilder builder)
        {
            var builtConfig = builder.Build();
            var keyVaultName = builtConfig.GetValue<string>("keyVaultName");

            if (keyVaultName == null)
            {
                return;
            }

            builder.AddAzureKeyVault(
                $"https://{keyVaultName}.vault.azure.net/",
                builtConfig.GetValue<string>("azKeyvaultClientId"),
                builtConfig.GetValue<string>("azKeyvaultClientSecret"));
        }
    }
}