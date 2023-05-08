using Domain.Entities.Products;
using FizzWare.NBuilder;
using Tests.Common;

namespace Tests.Unit.Products
{
    public class ProductRepositoryTests : PostgresDatabaseFixture
    {
        public ProductRepositoryTests() 
        {
        }

        [Fact]
        public async void AddNewProductAsync()
        {
            var repository = GetProductRepository();

            Assert.NotNull(repository);

            Product productMock = new()
            {
                Name = Faker.Name.FullName(),
                Price = Faker.RandomNumber.Next(0, 100),
                Description = Faker.Lorem.Sentence(),
                Code = Faker.RandomNumber.Next(1000000, 3000000).ToString()
            };

            await repository.AddAsync(productMock);

            var productDb = _dbContext.Products.FirstOrDefault(a => a.Code == productMock.Code);

            Assert.NotNull(productDb);
            Assert.Equal(productMock.Name, productDb.Name);
            Assert.Equal(productMock.Price, productDb.Price);
            Assert.Equal(productMock.Description, productDb.Description);
            Assert.Equal(productMock.Code, productDb.Code);
        }

        [Fact]
        public async void UpdateProductAsync()
        {
            var repository = GetProductRepository();

            Assert.NotNull(repository);

            Product productMock = new()
            {
                Name = Faker.Name.FullName(),
                Price = Faker.RandomNumber.Next(0, 100),
                Description = Faker.Lorem.Sentence(),
                Code = Faker.RandomNumber.Next(1000000, 3000000).ToString()
            };

            await repository.AddAsync(productMock);

            var productDb = _dbContext.Products.FirstOrDefault(a => a.Code == productMock.Code);

            Assert.NotNull(productDb);
            Assert.Equal(productMock.Name, productDb.Name);
            Assert.Equal(productMock.Price, productDb.Price);
            Assert.Equal(productMock.Description, productDb.Description);
            Assert.Equal(productMock.Code, productDb.Code);

            productMock.Name = Faker.Name.FullName();
            productMock.Price = Faker.RandomNumber.Next(0, 100);
            productMock.Description = Faker.Lorem.Sentence();
            productMock.Code = Faker.RandomNumber.Next(1000000, 3000000).ToString();

            await repository.UpdateAsync(productMock);

            Assert.NotNull(productDb);
            Assert.Equal(productMock.Name, productDb.Name);
            Assert.Equal(productMock.Price, productDb.Price);
            Assert.Equal(productMock.Description, productDb.Description);
            Assert.Equal(productMock.Code, productDb.Code);
        }

        [Theory]
        [InlineData(10)]
        public async void GetAllProductsAsync(int size)
        {
            var repository = GetProductRepository();

            Assert.NotNull(repository);

            var productsMockedList = Builder<Product>.CreateListOfSize(size)
                .All()
                .With(p => p.Name = Faker.Name.FullName())
                .With(p => p.Price = Faker.RandomNumber.Next(0, 100))
                .With(p => p.Description = Faker.Lorem.Sentence())
                .With(p => p.Code = Faker.RandomNumber.Next(1000000, 3000000).ToString())
                .Build();

            foreach (var product in productsMockedList)
                await repository.AddAsync(product);

            var productsDbList = await repository.GetAllAsync();

            Assert.NotNull(productsDbList);
            Assert.NotEmpty(productsDbList);
            Assert.Equal(size, productsDbList.Count());
            Assert.Equal(productsMockedList.Count(), productsDbList.Count());
        }

        [Fact]
        public async void GetProductByIdAsync()
        {
            var repository = GetProductRepository();

            Assert.NotNull(repository);

            Product productMock = new()
            {
                Name = Faker.Name.FullName(),
                Price = Faker.RandomNumber.Next(0, 100),
                Description = Faker.Lorem.Sentence(),
                Code = Faker.RandomNumber.Next(1000000, 3000000).ToString()
            };

            var newProduct = await repository.AddAsync(productMock);

            var productDb = await repository.GetByIdAsync(newProduct.Id);

            Assert.NotNull(productDb);
            Assert.Equal(productMock.Name, productDb.Name);
            Assert.Equal(productMock.Price, productDb.Price);
            Assert.Equal(productMock.Description, productDb.Description);
            Assert.Equal(productMock.Code, productDb.Code);
        }

        [Fact]
        public async void RemoveProductByIdAsync()
        {
            var repository = GetProductRepository();

            Assert.NotNull(repository);

            Product productMock = new()
            {
                Name = Faker.Name.FullName(),
                Price = Faker.RandomNumber.Next(0, 100),
                Description = Faker.Lorem.Sentence(),
                Code = Faker.RandomNumber.Next(1000000, 3000000).ToString()
            };

            var newProduct = await repository.AddAsync(productMock);

            var productDb = await repository.GetByIdAsync(newProduct.Id);

            Assert.NotNull(productDb);
            Assert.Equal(productMock.Name, productDb.Name);
            Assert.Equal(productMock.Price, productDb.Price);
            Assert.Equal(productMock.Description, productDb.Description);
            Assert.Equal(productMock.Code, productDb.Code);

            await repository.RemoveAsync(newProduct.Id);

            var productNotFoundDb = await repository.GetByIdAsync(newProduct.Id);
            Assert.Null(productNotFoundDb);
        }
    }
}
