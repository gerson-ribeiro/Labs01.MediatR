using Labs01.MediatR.ProductContext.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs01.MediatR.ProductContext.Application.Queries.GetProductById
{
    public class GetProductByIdQuery :IRequest<ProductModel>
    {
        public int Id { get; set; }
    }
}
