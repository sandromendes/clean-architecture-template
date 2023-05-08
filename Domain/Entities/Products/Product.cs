using Domain.Common;
using Domain.Common.Interface;
using Domain.Interfaces.Products;

namespace Domain.Entities.Products
{
    public class Product : IdentityEntity, IIdentityEntity, IProduct
    {
        public string? Code { get; set; }
        public string? Name { get; set; }    
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
