using Labs01.MediatR.Commons.Configurations.NotificationContext;
using Labs01.MediatR.ProductContext.Persistence;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Labs01.MediatR.ProductContext.Application.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, bool>
    {
        private readonly ProductDbContext _dbContext;
        private readonly NotificationContext _notificationContext;

        public DeleteProductCommandHandler(ProductDbContext dbContext, NotificationContext notificationContext)
        {
            _dbContext = dbContext;
            _notificationContext = notificationContext;
        }

        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _dbContext.Product
                .FirstOrDefault(x => x.Id == request.Id);

            if (entity == null)
                _notificationContext.AddNotification(nameof(InvalidOperationException), "Produto não encontrado na base de dados!");

            _dbContext.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
