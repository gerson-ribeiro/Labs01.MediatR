using Labs01.MediatR.ProductContext.Application.Models;
using Labs01.MediatR.ProductContext.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Labs01.MediatR.ProductContext.Application.Queries.GetProductList
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, List<ProductModel>>
    {
        private readonly ProductDbContext _dbContext;

        public GetProductListQueryHandler(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<List<ProductModel>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Product.AsQueryable();

            if (!string.IsNullOrEmpty(request.ProductName))
            {
                query = query.Where(x => x.ProductName.Contains(request.ProductName));
            }

            var results = query.Select(x => new ProductModel
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Brand = x.Brand,
                Price = x.Price
            }).ToList();

            return Task.FromResult(results);
        }
    }
}
