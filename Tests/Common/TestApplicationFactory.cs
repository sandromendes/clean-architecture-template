using Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Tests.Common
{
    public class TestApplicationFactory : WebApplicationFactory<Program>, IDisposable
    {
        public TestApplicationFactory()
        {
            var _dbContext = new ApplicationDbContext(GetContextOptions());
            _dbContext?.Database.Migrate();
        }

        private DbContextOptions<ApplicationDbContext> GetContextOptions()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Tests.json")
                .Build();

            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql($@"
                    User ID=postgres;
                    Password=postgres;
                    Host=localhost;
                    Port=5432;
                    Database={configuration.GetConnectionString("ApplicationDbConnection")};
                    Pooling=true;")
                .Options;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Tests.json")
                .Build();

            builder.ConfigureAppConfiguration(config =>
            {
                config.AddConfiguration(configuration);
            });

            Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Program>();
            });
        }

        public void Dispose()
        {
            var _dbContext = new ApplicationDbContext(GetContextOptions());
            _dbContext?.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(ApplicationDbContext.Products)}\"");
            //_dbContext?.Database.ExecuteSqlRaw($"DROP DATABASE IF EXISTS \"{_dbContext?.Database.GetDbConnection().Database}\"");
        }
    }
}
