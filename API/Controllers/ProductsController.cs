using Application.Products.CreateProduct;
using Application.Products.GetAllProducts;
using Application.Products.GetProduct;
using Application.Products.RemoveProduct;
using Application.Products.UpdateProduct;
using Infrastructure.Repositories.Products.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllProductsResponse>>> GetAllProducts()
        {
            var query = new GetAllProductsRequest();

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetByIdAsync(Guid productId)
        {
            var response = await _mediator.Send(new GetProductRequest(productId));

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CreateProductRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> PutAsync([FromBody] UpdateProductFromBodyRequest request, Guid productId)
        {
            var query = new UpdateProductRequest
            {
                Id = productId,
                Code = request.Code,
                Description = request.Description,
                Name = request.Name,
                Price = request.Price
            };

            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAsync([FromQuery] RemoveProductRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
