using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XIVRepo.Core.Models.Accounts;

namespace XIVRepo.Core.Models.Mods
{
    public class Mod
    {
        [Key]
        public Guid ModId { get; set; }
        public Account Author { get; set; }
        public string Title { get; set; }
        public Status ModStatus { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime PublishedTime { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; }
        public bool IsNSFW { get; set; }
        public List<Category> Categories { get; set; }
        public List<Tags> UserTags { get; set; }
    }
}