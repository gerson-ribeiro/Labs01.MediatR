using Labs01.MediatR.Commons.Configuration.MediatrConfigurations;
using Labs01.MediatR.Commons.Configuration.MediatrConfigurations.Pipelines;
using Labs01.MediatR.ProductContext.Persistence;
using Labs01.MediatR.WebApp.MediatrConfigurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Labs01.MediatR.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<ProductDbContext>();

            services.AddMvc(options => options.Filters.Add<NotificationPipeline>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.ConfigureServices()
                .MediatRConfigurations();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
