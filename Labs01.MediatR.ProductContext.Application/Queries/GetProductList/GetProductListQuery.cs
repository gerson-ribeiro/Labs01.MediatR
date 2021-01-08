using Labs01.MediatR.ProductContext.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs01.MediatR.ProductContext.Application.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<List<ProductModel>>
    {
        public string ProductName { get; set; }
    }
}
