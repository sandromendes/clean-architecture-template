using Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Purchases
{
    public class Cart
    {
        public List<Product> Products { get; set; }
    }
}
