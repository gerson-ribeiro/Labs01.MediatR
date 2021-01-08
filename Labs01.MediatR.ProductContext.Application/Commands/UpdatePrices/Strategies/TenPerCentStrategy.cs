﻿using Labs01.MediatR.Commons.Configurations.NotificationContext;
using Labs01.MediatR.ProductContext.Domain.Entities;
using Labs01.MediatR.ProductContext.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Labs01.MediatR.ProductContext.Application.Commands.UpdatePrices.Strategies
{
    public class TenPerCentStrategy : IProductDiscountStrategy
    {
        public IProductDiscountStrategy Next { get; protected set; }
        private readonly ProductDbContext _dbContext;

        public TenPerCentStrategy(IProductDiscountStrategy next, ProductDbContext dbContext)
        {
            Next = next;
            _dbContext = dbContext;
        }

        public async Task<bool> Handler(Product product, double discount)
        {
            if(discount > 10)
            {
                return await Next.Handler(product,discount);
            }

            product.Price -= (discount / 100) * product.Price;

            _dbContext.Product.Add(product);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
