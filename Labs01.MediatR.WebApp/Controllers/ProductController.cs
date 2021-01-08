using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labs01.MediatR.ProductContext.Application.Commands.CreateProduct;
using Labs01.MediatR.ProductContext.Application.Commands.DeleteProduct;
using Labs01.MediatR.ProductContext.Application.Commands.UpdatePrices;
using Labs01.MediatR.ProductContext.Application.Commands.UpdateProduct;
using Labs01.MediatR.ProductContext.Application.Queries.GetProductById;
using Labs01.MediatR.ProductContext.Application.Queries.GetProductList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Labs01.MediatR.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator MediatR;

        public ProductController(IMediator mediatR)
        {
            MediatR = mediatR;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await MediatR.Send(new GetProductListQuery());

            return Ok(results);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var results = await MediatR.Send(new GetProductByIdQuery
            {
                Id = id
            });

            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProductCommand command)
        {
            var results = await MediatR.Send(command);

            return Ok(results);
        }

        [HttpPost("{id}/{discount}")]
        public async Task<IActionResult> Post([FromRoute] int id, [FromRoute] double discount)
        {
            var results = await MediatR.Send(new UpdateProductPriceCommand
            {
                DiscountPerCent = discount,
                ProductId = id
            });

            return Ok(results);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProductCommand command)
        {
            var results = await MediatR.Send(command);

            return Ok(results);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProductCommand command)
        {
            var results = await MediatR.Send(command);

            return Ok(results);
        }
    }
}
