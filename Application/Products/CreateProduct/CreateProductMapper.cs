using AutoMapper;
using Domain.Entities.Products;

namespace Application.Products.CreateProduct
{
    public class CreateProductMapper : Profile
    {
        public CreateProductMapper() 
        {
            CreateMap<CreateProductRequest, Product>();
        }
    }
}
