using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XIVRepo.Core.Models.Mods
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public ICollection<Mod> ModsInCategory { get; set; }
    }
}