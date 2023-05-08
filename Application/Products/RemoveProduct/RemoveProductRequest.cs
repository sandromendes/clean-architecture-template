using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.RemoveProduct
{
    public class RemoveProductRequest : IRequest<RemoveProductResponse>
    {
        public Guid ProductId { get; set; }
    }
}
