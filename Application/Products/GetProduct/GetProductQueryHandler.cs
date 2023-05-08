using AutoMapper;
using Domain.Repositories.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.GetProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductRequest, GetProductResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }   

        public async Task<GetProductResponse?> Handle(GetProductRequest request, CancellationToken cancellationToken)
        {

            var productDb = await _repository
                .AsQueryable()
                .Where(a => a.Id == request.ProductId)
                .FirstOrDefaultAsync();

            if (productDb == null) { return null; }

            return _mapper.Map<GetProductResponse>(productDb);
        }
    }
}
