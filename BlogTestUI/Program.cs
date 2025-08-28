using BlogDataLibrary.Data;
using BlogDataLibrary.Database;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace BlogTestUI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();

            // Setup dependency injection
            var serviceProvider = new ServiceCollection()
                .AddSingleton(config)
                .AddTransient<ISqlDataAccess, SqlDataAccess>()
                .AddTransient<SqlData>()
                .BuildServiceProvider();

            // Test the setup
            Console.WriteLine("Blog Test UI Started");
            Console.WriteLine("====================");

            try
            {
                // Get the SqlData service
                var sqlData = serviceProvider.GetRequiredService<SqlData>();

                Console.WriteLine("Dependency injection setup successful!");
                Console.WriteLine("Configuration loaded successfully!");
                Console.WriteLine($"Connection string found: {config.GetConnectionString("Default") != null}");

                // You can add more testing code here in future exercises
                Console.WriteLine("\nReady for database operations...");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during setup: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}