using System.Linq;
using HotChocolate;
using HotChocolate.AspNetCore.Authorization;
using HotChocolate.Data;
using HotChocolate.Types;
using XIVRepo.Core.Models.Accounts;
using XIVRepo.EntityFramework;

namespace XIVRepo.GraphQL.Services
{
    public class AccountService
    {
        [ExtendObjectType(Name = "Query")]
        public class Queries
        {
            [UseDbContext(typeof(XivRepoDbContext))]
            public IQueryable<Account> GetAccounts([ScopedService] XivRepoDbContext context) => context.UserAccounts;
        }
    }
}