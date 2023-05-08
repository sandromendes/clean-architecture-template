using Application.Products;
using Application.Products.CreateProduct;
using Application.Products.GetProduct;
using Bogus;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Tests.Common;

namespace Tests.Integration.Products
{
    public class ProductEndpointTests : 
        IClassFixture<TestApplicationFactory>, 
        IAsyncLifetime
    {
        private readonly HttpClient _httpClient;

        public ProductEndpointTests(TestApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }

        public virtual Task DisposeAsync() 
        {
            return Task.CompletedTask;
        }

        public virtual Task InitializeAsync()
        {
            return Task.CompletedTask;
        }


        [Fact]
        public async Task GetAllProductsTest()
        {
            var response = await _httpClient.GetAsync("api/products");

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task GetProductByIdTest()
        {
            var newProduct = new Faker<ProductDto>()
                .RuleFor(p => p.Id, p => p.Random.Guid())
                .RuleFor(p => p.Code, p => p.Random.AlphaNumeric(20))
                .RuleFor(p => p.Name, p => p.Commerce.Product())
                .RuleFor(p => p.Description, p => p.Commerce.ProductDescription())
                .RuleFor(p => p.Price, p => p.Random.Decimal())
                .Generate();

            var newProductSerialized = JsonSerializer.Serialize(newProduct);

            var requestContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            var postResponse = await _httpClient.PostAsync("api/products", requestContent);

            postResponse.EnsureSuccessStatusCode();

            var postProductResponse = await postResponse.Content.ReadFromJsonAsync<CreateProductResponse>();

            var getResponse = await _httpClient.GetAsync($"api/products/{postProductResponse?.product?.Id}");

            getResponse.EnsureSuccessStatusCode();

            var getProductResponse = await getResponse.Content.ReadFromJsonAsync<GetProductResponse>();

            Assert.NotNull(getProductResponse);
            Assert.Equal(getProductResponse.Name, newProduct.Name);
            Assert.Equal(getProductResponse.Code, newProduct.Code);
            Assert.Equal(getProductResponse.Description, newProduct.Description);
            Assert.Equal(getProductResponse.Price, newProduct.Price);
        }

        [Fact]
        public async Task CreateProductTest()
        {
            var newProduct = new Faker<ProductDto>()
                .RuleFor(p => p.Id, p => p.Random.Guid())
                .RuleFor(p => p.Code, p => p.Random.AlphaNumeric(20))
                .RuleFor(p => p.Name, p => p.Commerce.Product())
                .RuleFor(p => p.Description, p => p.Commerce.ProductDescription())
                .RuleFor(p => p.Price, p => p.Random.Decimal())
                .Generate();

            var newProductSerialized = JsonSerializer.Serialize(newProduct);

            var requestContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/products", requestContent);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task UpdateProductTest()
        {
            var newProduct = new Faker<ProductDto>()
                .RuleFor(p => p.Id, p => p.Random.Guid())
                .RuleFor(p => p.Code, p => p.Random.AlphaNumeric(20))
                .RuleFor(p => p.Name, p => p.Commerce.Product())
                .RuleFor(p => p.Description, p => p.Commerce.ProductDescription())
                .RuleFor(p => p.Price, p => p.Random.Decimal())
                .Generate();

            var newProductSerialized = JsonSerializer.Serialize(newProduct);

            var postContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            var postResponse = await _httpClient.PostAsync("api/products", postContent);

            postResponse.EnsureSuccessStatusCode();

            var productResponse = await postResponse.Content.ReadFromJsonAsync<CreateProductResponse>();

            var updatedProduct = new Faker<ProductDto>()
                .RuleFor(p => p.Id, p => p.Random.Guid())
                .RuleFor(p => p.Code, p => p.Random.AlphaNumeric(20))
                .RuleFor(p => p.Name, p => p.Commerce.Product())
                .RuleFor(p => p.Description, p => p.Commerce.ProductDescription())
                .RuleFor(p => p.Price, p => p.Random.Decimal())
                .Generate();

            var updatedProductSerialized = JsonSerializer.Serialize(updatedProduct);

            var putContent = new StringContent(updatedProductSerialized, Encoding.UTF8, "application/json");

            var putResponse = await _httpClient.PutAsync($"api/products/{productResponse?.product?.Id}", putContent);

            putResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task RemoveProductTest()
        {
            var newProduct = new Faker<ProductDto>()
                .RuleFor(p => p.Id, p => p.Random.Guid())
                .RuleFor(p => p.Code, p => p.Random.AlphaNumeric(20))
                .RuleFor(p => p.Name, p => p.Commerce.Product())
                .RuleFor(p => p.Description, p => p.Commerce.ProductDescription())
                .RuleFor(p => p.Price, p => p.Random.Decimal())
                .Generate();

            var newProductSerialized = JsonSerializer.Serialize(newProduct);

            var postContent = new StringContent(newProductSerialized, Encoding.UTF8, "application/json");

            var postResponse = await _httpClient.PostAsync("api/products", postContent);

            postResponse.EnsureSuccessStatusCode();

            var productResponse = await postResponse.Content.ReadFromJsonAsync<CreateProductResponse>();

            var deleteResponse = await _httpClient.DeleteAsync($"api/products?productId={productResponse?.product?.Id}");

            deleteResponse.EnsureSuccessStatusCode();

            var getResponse = await _httpClient.GetAsync($"api/products/{productResponse?.product?.Id}");

            getResponse.EnsureSuccessStatusCode();

            Assert.Equal(HttpStatusCode.NoContent, getResponse.StatusCode);
        }
    }
}
