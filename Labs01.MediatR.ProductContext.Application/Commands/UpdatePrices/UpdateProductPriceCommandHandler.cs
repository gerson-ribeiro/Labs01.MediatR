using Labs01.MediatR.ProductContext.Application.Commands.UpdatePrices.Strategies;
using Labs01.MediatR.ProductContext.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Labs01.MediatR.ProductContext.Application.Commands.UpdatePrices
{
    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand, bool>
    {
        private readonly IProductDiscountStrategy _strategy;
        private readonly ProductDbContext _dbContext;

        public UpdateProductPriceCommandHandler(IProductDiscountStrategy strategy, ProductDbContext dbContext)
        {
            _strategy = strategy;
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            var product = _dbContext.Product.Find(request.ProductId);

            return await _strategy.Handler(product, request.DiscountPerCent);
        }
    }
}
