using Labs01.MediatR.Commons.Configurations.NotificationContext;
using Labs01.MediatR.ProductContext.Persistence;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Labs01.MediatR.ProductContext.Application.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly ProductDbContext _dbContext;
        private readonly NotificationContext _notificationContext;

        public UpdateProductCommandHandler(ProductDbContext dbContext, NotificationContext notificationContext)
        {
            _dbContext = dbContext;
            _notificationContext = notificationContext;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Product
                .FirstOrDefault(x => x.Id == request.Id);

            if (entity == null)
                _notificationContext.AddNotification(nameof(InvalidOperationException), "Produto não encontrado na base de dados!");

            entity.ProductName = request.ProductName;
            entity.Brand = request.Brand;
            entity.Args = request.Args;
            entity.Active = request.Active;

            _dbContext.Update(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
