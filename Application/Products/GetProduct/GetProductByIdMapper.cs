using AutoMapper;
using Domain.Entities.Products;

namespace Application.Products.GetProduct
{
    public class GetProductByIdMapper : Profile
    {
        public GetProductByIdMapper()
        {
            CreateMap<Product, GetProductResponse>();
        }
    }
}
