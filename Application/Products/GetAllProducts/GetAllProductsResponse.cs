using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.GetAllProducts
{
    public class GetAllProductsResponse
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
