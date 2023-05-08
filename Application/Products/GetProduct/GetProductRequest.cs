using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Products.GetProduct
{
    public class GetProductRequest : IRequest<GetProductResponse>
    {
        public Guid ProductId { get; set; }
        public GetProductRequest(Guid productId)
        {
            ProductId = productId;
        }
    }
}
