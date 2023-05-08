using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.UpdateProduct
{
    public class UpdateProductResponse
    {
        public string Message { get; set; }

        public UpdateProductResponse(string message)
        {
            Message = message;
        }
    }
}
