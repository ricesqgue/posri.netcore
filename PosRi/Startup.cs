using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PosRi.Entities;
using PosRi.Entities.Context;
using PosRi.Helpers.Extensions;
using PosRi.Services;
using PosRi.Services.Contracts;
using PosRi.Services.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace PosRi
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddMvc();

            var connectionString = @"Server=MSI\SQLExpress;Database=PosRi;Trusted_Connection=True;";
            services.AddDbContext<PosRiContext>(o => o.UseSqlServer(connectionString));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "PosRi API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme() { In = "header", Description = "Please insert JWT with bearer into field", Name = "Authorization", Type = "apiKey" });
            });

            //Dependency Injection
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IStoreRepository, StoreRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICashRegisterRepository, CashRegisterRepository>();
            services.AddTransient<IBrandRepository, BrandRepository>();
            services.AddTransient<IColorRepository, ColorRepository>();
            services.AddTransient<ISizeRepository, SizeRepository>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddTransient<IClientRepository, ClientRepository>();
            services.AddTransient<IVendorRepository, VendorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, PosRiContext posRiContext)
        {
            loggerFactory.AddConsole();
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

           posRiContext.EnsureSeedDataForContext();


            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            // AutoMapper
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Entities.User, Models.Response.UserDto>();
                cfg.CreateMap<Entities.Role, Models.Response.RoleDto>();
                cfg.CreateMap<Entities.Store, Models.Response.StoreDto>();
                cfg.CreateMap<Entities.Category, Models.Response.CategoryDto>();
                cfg.CreateMap<Entities.SubCategory, Models.Response.SubCategoryDto>();
                cfg.CreateMap<Entities.CashRegister, Models.Response.CashRegisterDto>();
                cfg.CreateMap<Entities.Brand, Models.Response.BrandDto>();
                cfg.CreateMap<Entities.Color, Models.Response.ColorDto>();
                cfg.CreateMap<Entities.Size, Models.Response.SizeDto>();
                cfg.CreateMap<Entities.State, Models.Response.StateDto>();
                cfg.CreateMap<Entities.Client, Models.Response.ClientDto>();
                cfg.CreateMap<Entities.Vendor, Models.Response.VendorDto>();
            });

            //

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
