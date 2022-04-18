using Klir.TechChallenge.Domain.Interface.Repository;
using Klir.TechChallenge.Domain.Interface.Service;
using Klir.TechChallenge.Domain.Service;
using Klir.TechChallenge.InfraStructure.ContextModel;
using Klir.TechChallenge.InfraStructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KlirTechChallenge.Web.Api
{
    public class Startup
    {
        readonly string AllowSpecificOrigins = "_allowSpecificOrigins";//_allowSpecificOrigins

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:4200")
                                      .AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
                                  });
            });

            services.AddControllers();
            services.AddSwaggerGen();

            //region to add dependency injection
            //context
            //services.AddScoped<KlirContext>();
            services.AddDbContext<KlirContext>(
                        options => options.UseSqlServer(Configuration.GetSection("ConnectionStrings").GetSection("Main").Value));
            //repository
            services.AddScoped<IPromotionRepository, PromotionRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IGroupShoopingCartRepository, GroupShoopingCartRepository>();
            //services
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IGroupShoppingCartService, GroupShoppingCartService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(AllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Test1 Api v1");
            });


        }
    }
}
