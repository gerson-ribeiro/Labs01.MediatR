using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs01.MediatR.ProductContext.Application.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Args { get; set; }
        public bool Active { get; set; }
    }
}
