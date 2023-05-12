using MediatR;

namespace Application.Products.CreateProduct
{
    public class CreateProductRequest : IRequest<CreateProductResponse>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
