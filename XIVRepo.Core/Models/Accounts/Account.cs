using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XIVRepo.Core.Models.Accounts
{
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }
        public string DiscordId { get; set; }
        public string DisplayName { get; set; }
        public ICollection<Role>? Roles { get; set; }
    }
}