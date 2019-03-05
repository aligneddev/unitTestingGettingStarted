using System;
using Api;
using Api.Weather;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Api.Integration.Tests
{
    public class BikeTrackingApiWebApplicationFactory<TStartup> : WebApplicationFactory<Startup>
    { 
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

                // Add a database context (AppDbContext) using an in-memory database for testing.
                //    services.AddDbContext<AppDbContext>(options =>
                //{
                //    options.UseInMemoryDatabase("InMemoryAppDb");
                //    options.UseInternalServiceProvider(serviceProvider);
                //});

                services.AddHttpClient<IGetWeatherHttpClient, ApixuClient>();

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    //var appDb = scopedServices.GetRequiredService<AppDbContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<BikeTrackingApiWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    // appDb.Database.EnsureCreated();

                    //try
                    //{
                    //    // Seed the database with some specific test data.
                    //    SeedData.PopulateTestData(appDb);
                    //}
                    //catch (Exception ex)
                    //{
                    //    logger.LogError(ex, "An error occurred seeding the " +
                    //                        "database with test messages. Error: {ex.Message}");
                    //}
                }
            });
        }
    }
}
