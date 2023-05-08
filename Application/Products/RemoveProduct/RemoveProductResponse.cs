using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.RemoveProduct
{
    public class RemoveProductResponse
    {
        public string Message { get; set; }

        public RemoveProductResponse(string message) { Message = message; }  
    }
}
