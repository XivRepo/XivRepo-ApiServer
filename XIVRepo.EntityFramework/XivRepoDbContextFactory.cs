using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using XIVRepo.Core.Helpers;

namespace XIVRepo.EntityFramework
{
    public class XivRepoDbContextFactory : IDesignTimeDbContextFactory<XivRepoDbContext>
    {
        public XivRepoDbContext CreateDbContext(string[] args)
        {
            DotNetEnv.Env.Load();
            DotNetEnv.Env.TraversePath().Load();
            
            var dbConnectionString =
                $"Server=localhost;Port=3306;Database={EnvironmentVariables.DatabaseName()};Uid={EnvironmentVariables.DatabaseUserId()};Pwd={EnvironmentVariables.DatabasePassword()};";
            var builder = new DbContextOptionsBuilder<XivRepoDbContext>()
                .UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString))
                .EnableDetailedErrors();
            
            return new XivRepoDbContext(builder.Options);
        }
    }
}