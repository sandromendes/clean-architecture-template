using AutoMapper;
using Domain.Entities.Products;

namespace Application.Products.GetAllProducts
{
    public class GetAllProductsMapper : Profile
    {
        public GetAllProductsMapper()
        {
            CreateMap<Product, ProductDto>();
        }
    }
}
