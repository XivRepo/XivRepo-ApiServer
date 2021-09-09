using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XIVRepo.Core.Models.Mods
{
    public class ModFollowers
    {
        public Guid ModId { get; set; }
        public Guid FollowerId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime TimeFollowed { get; set; }
    }
}