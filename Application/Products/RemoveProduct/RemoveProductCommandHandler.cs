using Domain.Repositories.Products;
using MediatR;

namespace Application.Products.RemoveProduct
{
    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductRequest, RemoveProductResponse>
    {
        private readonly IProductRepository _repository;

        public RemoveProductCommandHandler(IProductRepository repository) {  _repository = repository; }

        public async Task<RemoveProductResponse> Handle(RemoveProductRequest request, CancellationToken cancellationToken)
        {
            var productDb = await _repository.GetByIdAsync(request.ProductId);

            if (productDb == null)
                return new RemoveProductResponse("Product not found!");

            await _repository.RemoveAsync(productDb.Id);

            return new RemoveProductResponse($"Product {request.ProductId} was removed from database!");
        }
    }
}
