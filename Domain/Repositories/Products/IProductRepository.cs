using Domain.Entities.Products;
using Domain.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Products
{
    public interface IProductRepository : IDomainRepository<Product>
    {
    }
}
