using Labs01.MediatR.Commons.Configurations.NotificationContext;
using Labs01.MediatR.ProductContext.Persistence;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Labs01.MediatR.ProductContext.Application.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly ProductDbContext _dbContext;
        private readonly NotificationContext _notificationContext;

        public CreateProductCommandHandler(ProductDbContext dbContext, NotificationContext notificationContext)
        {
            _dbContext = dbContext;
            _notificationContext = notificationContext;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Product
                .FirstOrDefault(x => x.ProductName.Equals(request.ProductName) && x.Brand.Equals(request.Brand));

            if (entity != null)
            {
                _notificationContext.AddNotification(nameof(InvalidOperationException), "Já existe um produto cadastrado com este nome e marca!");
                return 0;
            }

            entity = new Domain.Entities.Product
            {
                ProductName = request.ProductName,
                Brand = request.Brand,
                Price = request.Price,
                Active = true,
                Args = request.Args
            };

            _dbContext.Add(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
