using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XIVRepo.Core.Models.Mods
{
    public class Tag
    {
        [Key]
        public Guid TagId { get; set; }
        public string Title { get; set; }
        public ICollection<Mod>? ModsWithTag { get; set; }
    }
}