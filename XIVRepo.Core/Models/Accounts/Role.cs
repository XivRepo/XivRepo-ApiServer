using System;
using System.Collections;
using System.Collections.Generic;

namespace XIVRepo.Core.Models.Accounts
{
    public class Role
    {
        public Guid RoleId { get; set; }
        public string Title { get; set; }
        //TODO: Fix Join Table for Roles
        public ICollection<Account> AccountsWithRole { get; set; }
    }
}