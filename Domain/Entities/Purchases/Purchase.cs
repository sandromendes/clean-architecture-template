using Domain.Common.Interface;
using Domain.Common;
using Domain.Entities.Products;
using Domain.Entities.Customers;

namespace Domain.Entities.Purchases
{
    public class Purchase : IdentityEntity, IIdentityEntity
    {
        public Guid Id { get; set; }
        public List<Product> Products { get; set; }
        public DateTime Created { get; set; }
        public Customer Customer { get; set; }


    }
}
