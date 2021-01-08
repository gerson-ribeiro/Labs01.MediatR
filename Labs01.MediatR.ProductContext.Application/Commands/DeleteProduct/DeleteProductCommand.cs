using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs01.MediatR.ProductContext.Application.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
