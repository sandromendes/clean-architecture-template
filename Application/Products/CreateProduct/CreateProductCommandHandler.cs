using AutoMapper;
using Domain.Entities.Products;
using Domain.Repositories.Products;
using MediatR;

namespace Application.Products.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductRequest, CreateProductResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CreateProductResponse> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            var newProduct = _mapper.Map<Product>(request);
            
            newProduct.CreatedDate = DateTime.Now;
            newProduct.UpdatedDate = DateTime.Now;
            newProduct.IsActive = true;


            var productDb = await _repository.AddAsync(newProduct);

            var productDto = _mapper.Map<ProductDto>(productDb);

            return new CreateProductResponse { product = productDto };
        }
    }
}
