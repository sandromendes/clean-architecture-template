using AutoMapper;
using Domain.Repositories.Products;
using MediatR;

namespace Application.Products.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductRequest, UpdateProductResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper) 
        { 
            _repository = repository; 
            _mapper = mapper;
        }

        public async Task<UpdateProductResponse> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var productDb = _repository.GetByIdAsync(request.Id).Result;

            if (productDb == null) return new UpdateProductResponse("Product not found!");
            
            if (!string.IsNullOrEmpty(request.Code)) productDb.Code = request.Code;
            if (!string.IsNullOrEmpty(request.Name)) productDb.Name = request.Name;
            if (!string.IsNullOrEmpty(request.Description)) productDb.Description = request.Description;

            productDb.Price = request.Price;

            await _repository.UpdateAsync(productDb);

            return new UpdateProductResponse($"Product {productDb.Id} was updated!");
        }
    }
}
