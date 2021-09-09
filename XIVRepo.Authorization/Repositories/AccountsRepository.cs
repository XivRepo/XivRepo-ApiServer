using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XIVRepo.Core.Models.Accounts;
using XIVRepo.EntityFramework;

namespace XIVRepo.Authorization.Repositories
{
    public class AccountsRepository : XivRepoBaseRepository
    {
        public AccountsRepository(XivRepoDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<Account> AddAccount(Account newAccount)
        {
            var account = await Context.UserAccounts.AddAsync(newAccount);
            await Context.SaveChangesAsync();
            return account.Entity;
        }

        public async Task<Account> GetDefaultAccount()
        {
            return await Context.UserAccounts
                .Where(a => a.Id == Guid.Parse("8b3cc4c0-46d5-47df-8bc3-2d7672a2dd70"))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await Context.Roles.ToListAsync();
        }
    }
}