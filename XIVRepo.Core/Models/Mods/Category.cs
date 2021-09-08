using System;
using System.ComponentModel.DataAnnotations;

namespace XIVRepo.Core.Models.Mods
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }
        public string Title { get; set; }
    }
}