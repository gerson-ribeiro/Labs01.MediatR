using Labs01.MediatR.ProductContext.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Labs01.MediatR.ProductContext.Application.Commands.UpdatePrices.Strategies
{
    public interface IProductDiscountStrategy
    {
        IProductDiscountStrategy Next { get; }
        Task<bool> Handler(Product product, double discount);
    }
}
