namespace Infrastructure.Repositories.Products.Request
{
    public class UpdateProductFromBodyRequest
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
