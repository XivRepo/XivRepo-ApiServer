using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using XIVRepo.Core.Helpers;
using XIVRepo.EntityFramework;
using XIVRepo.GraphQL.Services;

namespace XIVRepo.GraphQL
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
            services.AddControllers();
            
            ConfigureAuthorization(services);
            SetupDatabaseServices(services);
            SetupGraphQLServices(services);
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
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Please checkout the playground or query the GraphQL API directly");
                });
            });
        }

        private void SetupGraphQLServices(IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddAuthorization()
                .AddQueryType(d => d.Name("Query"))
                .AddType<AccountService.Queries>();
        }
        
        private void SetupDatabaseServices(IServiceCollection services)
        {
            services.AddPooledDbContextFactory<XivRepoDbContext>(builder =>
            {
                var dbConnectionString = $"Server=localhost;Port=3306;Database={EnvironmentVariables.DatabaseName()};Uid={EnvironmentVariables.DatabaseUserId()};Pwd={EnvironmentVariables.DatabasePassword()};";
                builder
                    .UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString))
                    .UseLazyLoadingProxies()
                    .EnableDetailedErrors();
            });
        }
        
        private void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(EnvironmentVariables.JwtKey())),
                        ValidateIssuer = true,
                        ValidIssuer = EnvironmentVariables.JwtIssuer(),
                        ValidateAudience = true,
                        ValidAudience = EnvironmentVariables.JwtAudience(),
                    };
                });
        }
    }
}