using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using XIVRepo.Core.Helpers;
using XIVRepo.EntityFramework.Triggers;

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
            var builder = new DbContextOptionsBuilder<XivRepoDbContext>();
            ConfigureContextOptionsBuilder(builder);
            return new XivRepoDbContext(builder.Options);
        }

        public static void ConfigureContextOptionsBuilder(DbContextOptionsBuilder builder)
        {
            var dbConnectionString =
                $"Server=localhost;Port=3306;Database={EnvironmentVariables.DatabaseName()};Uid={EnvironmentVariables.DatabaseUserId()};Pwd={EnvironmentVariables.DatabasePassword()};";
            builder
                .UseMySql(dbConnectionString, ServerVersion.AutoDetect(dbConnectionString))
                .UseTriggers(optionsBuilder =>
                {
                    optionsBuilder.AddTrigger<ModVersionTrigger>();
                });
            
            if (EnvironmentVariables.InDevMode())
            {
                builder.EnableDetailedErrors();
            }
        }
    }
}