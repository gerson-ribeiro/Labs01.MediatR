using Labs01.MediatR.ProductContext.Application.Commands.CreateProduct;
using Labs01.MediatR.ProductContext.Application.Commands.UpdatePrices;
using Labs01.MediatR.ProductContext.Application.Commands.UpdatePrices.Strategies;
using Labs01.MediatR.ProductContext.Application.Queries.GetProductById;
using Labs01.MediatR.ProductContext.Application.Queries.GetProductList;
using Labs01.MediatR.WebApp.MediatrConfigurations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labs01.MediatR.Commons.Configuration.MediatrConfigurations
{
    public static class MediatrConfiguration
    {
        public static IServiceCollection MediatRConfigurations(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetProductListQueryHandler).Assembly);
            services.AddMediatR(typeof(GetProductByIdQueryHandler).Assembly);
            services.AddMediatR(typeof(CreateProductCommandHandler).Assembly);
            services.AddMediatR(typeof(UpdateProductPriceCommandHandler).Assembly);

            //services.Chain<IProductDiscountStrategy>()
            //    .Add<TenPerCentStrategy>()
            //    .Add<TwentyPerCentStrategy>()
            //    .Add<ThirtyPerCentStrategy>()
            //    .Add<FortyPerCentStrategy>()
            //    .Add<FiftyPerCentStrategy>()
            //    .Configure();

            return services;
        }
    }
}
