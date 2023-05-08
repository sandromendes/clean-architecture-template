using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Repositories.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Products.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsRequest, GetAllProductsResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetAllProductsResponse> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
        {
            var items = await _repository
                .AsQueryable()
                .Where(a => true)
                .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return new GetAllProductsResponse { Products = items };
        }
    }
}
