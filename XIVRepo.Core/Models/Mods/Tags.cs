using System;
using System.ComponentModel.DataAnnotations;

namespace XIVRepo.Core.Models.Mods
{
    public class Tags
    {
        [Key]
        public Guid TagId { get; set; }
        public string Title { get; set; }
    }
}