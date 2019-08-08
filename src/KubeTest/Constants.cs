using System;

namespace KubeTest
{
    public static class Constants
    {
        public static class Environments
        {
            public const string Development = nameof(Development);
            public const string Production = nameof(Production);
            public const string Staging = nameof(Staging);
            public static readonly string CurrentAspNetCoreEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            public static bool IsDevelopment() { return CurrentAspNetCoreEnv == Development; }

            public static bool IsStaging() { return CurrentAspNetCoreEnv == Staging; }

            public static bool IsProduction() { return CurrentAspNetCoreEnv == Production; }
        }
    }
}