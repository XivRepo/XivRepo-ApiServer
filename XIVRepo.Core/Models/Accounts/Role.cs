using System;
using System.Collections;
using System.Collections.Generic;

namespace XIVRepo.Core.Models.Accounts
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<Account>? AccountsWithRole { get; set; }
    }
}