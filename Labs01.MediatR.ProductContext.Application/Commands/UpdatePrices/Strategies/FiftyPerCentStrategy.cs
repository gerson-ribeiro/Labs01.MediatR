using Labs01.MediatR.Commons.Configurations.NotificationContext;
using Labs01.MediatR.ProductContext.Domain.Entities;
using Labs01.MediatR.ProductContext.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Labs01.MediatR.ProductContext.Application.Commands.UpdatePrices.Strategies
{
    public class FiftyPerCentStrategy : IProductDiscountStrategy
    {
        public IProductDiscountStrategy Next { get; protected set; }
        private readonly ProductDbContext _dbContext;
        private readonly NotificationContext _notificationContext;

        public FiftyPerCentStrategy(IProductDiscountStrategy next, ProductDbContext dbContext, NotificationContext notificationContext)
        {
            Next = next;
            _dbContext = dbContext;
            _notificationContext = notificationContext;
        }

        public async Task<bool> Handler(Product product, double discount)
        {
            if (discount > 50)
            {
                _notificationContext.AddNotification("Não foi possível aplicar o desconto solicitado!", "Desconto maior do que o possível!");

                return false;
            }

            product.Price -= (discount / 100) * product.Price;

            _dbContext.Product.Update(product);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
