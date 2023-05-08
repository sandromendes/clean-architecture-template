using Application.Context;
using Domain.Entities.Products;
using Domain.Repositories.Products;
using Infrastructure.Context;
using Infrastructure.Repositories.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Products
{
    public class ProductRepository : RepositoryAsync<Product>, IProductRepository
    {
        public ProductRepository(IApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
