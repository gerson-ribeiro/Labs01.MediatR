using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs01.MediatR.ProductContext.Application.Commands.UpdatePrices
{
    public class UpdateProductPriceCommand : IRequest<bool>
    {
        public int ProductId { get; set; }
        public double DiscountPerCent { get; set; }
    }
}
