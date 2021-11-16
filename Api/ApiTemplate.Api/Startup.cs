using System;
using System.Reflection;
using System.Reflection.Metadata;
using ApiTemplate.Application.Queries;
using ApiTemplate.Contract;
using ApiTemplate.Domain.Repository;
using ApiTemplate.Infrastructure.Repository;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ApiTemplateDbContext = ApiTemplate.Infrastructure.Database.ApiTemplateDbContext;

namespace ApiTemplate.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApiTemplateDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddAutoMapper(typeof(MappingProfile));
            //services.AddScoped<IUserService, UserService>();

            services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(GetAllUsersQuery).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(GetUserQuery).GetTypeInfo().Assembly);
            services.AddOptions();
            services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddValidatorsFromAssembly(typeof(AssemblyReference).Assembly);

            services.AddLogging();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "ApiTemplate.Api", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiTemplateDbContext dataContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiTemplate.Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            dataContext.Database.Migrate();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}