using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using XIVRepo.Core;
using XIVRepo.Core.Models.Accounts;

namespace XIVRepo.EntityFramework.Repositories
{
    public class AccountsRepository : XivRepoBaseRepository
    {
        public AccountsRepository(XivRepoDbContext dbContext) : base(dbContext)
        {
        }
        
        public async Task<Account> AddAccount(Account newAccount)
        {
            var account = await _context.UserAccounts.AddAsync(newAccount);
            await _context.SaveChangesAsync();
            return account.Entity;
        }

        public async Task<Account> GetDefaultAccount()
        {
            return await _context.UserAccounts
                .Where(a => a.AccountId == Guid.Parse("8b3cc4c0-46d5-47df-8bc3-2d7672a2dd70"))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Role>> GetAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }
    }
}