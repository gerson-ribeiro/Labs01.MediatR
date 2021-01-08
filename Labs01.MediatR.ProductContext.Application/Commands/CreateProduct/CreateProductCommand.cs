using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs01.MediatR.ProductContext.Application.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Brand { get; set; }
        public string Args { get; set; }
    }
}
