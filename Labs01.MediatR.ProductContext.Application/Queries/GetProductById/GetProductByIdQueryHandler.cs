using Labs01.MediatR.Commons.Configurations.NotificationContext;
using Labs01.MediatR.ProductContext.Application.Models;
using Labs01.MediatR.ProductContext.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Labs01.MediatR.ProductContext.Application.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductModel>
    {
        private readonly ProductDbContext _dbContext;
        private readonly NotificationContext _notificationContext;

        public GetProductByIdQueryHandler(ProductDbContext dbContext, NotificationContext notificationContext)
        {
            _dbContext = dbContext;
            _notificationContext = notificationContext;
        }

        public Task<ProductModel> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = _dbContext.Product.Where(x => x.Id == request.Id).Select(y => new ProductModel
            {
                Id = y.Id,
                Brand = y.Brand,
                Price = y.Price,
                ProductName = y.ProductName
            }).FirstOrDefault();

            if (product == null)
            {
                _notificationContext.AddNotification("NADA ENCONTRADO", "ID NÃO ENCONTRADO");
                return null;
            }

            return Task.FromResult(product);
        }
    }
}
