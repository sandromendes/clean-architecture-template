using Infrastructure.Context;
using Infrastructure.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Tests.Common
{
    public class PostgresDatabaseFixture : IDisposable
    {
        public readonly ApplicationDbContext _dbContext;
        private readonly ProductRepository _productRepository;

        public PostgresDatabaseFixture()
        {
            var host = Host.CreateDefaultBuilder().Build();
            var config = host.Services.GetRequiredService<IConfiguration>();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseNpgsql($@"
                    User ID=postgres;
                    Password=postgres;
                    Host=localhost;
                    Port=5432;
                    Database=CleanArchitectureReposioryDb;
                    Pooling=true;")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.Migrate();

            _productRepository = new ProductRepository(_dbContext);
        }

        public ProductRepository GetProductRepository() { return _productRepository; }

        public void Dispose()
        {
            _dbContext?.Database.ExecuteSqlRaw($"DELETE FROM \"{nameof(ApplicationDbContext.Products)}\"");
        }
    }
}
