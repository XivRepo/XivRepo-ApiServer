using Microsoft.EntityFrameworkCore;
using XIVRepo.Core.Models.Accounts;

namespace XIVRepo.EntityFramework
{
    public class XivRepoDbContext : DbContext
    {
        public DbSet<Account> UserAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        public XivRepoDbContext(DbContextOptions<XivRepoDbContext> options) : base(options) { }
    }
}