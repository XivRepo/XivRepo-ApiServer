using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using XIVRepo.Authorization.Repositories;
using XIVRepo.Core.Helpers;
using XIVRepo.EntityFramework;

namespace XIVRepo.Authorization
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
            DotNetEnv.Env.Load();
            DotNetEnv.Env.TraversePath().Load();
            
            services.AddDbContext<XivRepoDbContext>(builder =>
            {
                var dbConnectionString = $"Server=localhost;Port=3306;Database={EnvironmentVariables.DatabaseName()};Uid={EnvironmentVariables.DatabaseUserId()};Pwd={EnvironmentVariables.DatabasePassword()};";
                builder
                    .UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString))
                    .UseLazyLoadingProxies()
                    .EnableDetailedErrors();
            });
            services.AddScoped<AccountsRepository>();
            
            services.AddControllers().AddJsonOptions(options => 
                options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "XIVRepo.Authorization", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "XIVRepo.Authorization v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}