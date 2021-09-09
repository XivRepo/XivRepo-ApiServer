using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using XIVRepo.Core.Models.Accounts;
using XIVRepo.Core.Models.Files;

namespace XIVRepo.Core.Models.Mods
{
    public class Mod
    {
        // Basic Info
        public Guid Id { get; set; }
        public string Slug { get; set; }
        public Account Author { get; set; }
        public string Title { get; set; }
        public string IconUrl { get; set; }
        public ICollection<PreviewImage>? PreviewImages { get; set; }
        public Status ModStatus { get; set; }
        public string ShortDescription { get; set; }
        public string? FullDescription { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Tag>? UserTags { get; set; }
        public ICollection<Version> Versions { get; set; }
        public ICollection<Mod> ModDependencies { get; set; }
        public ICollection<Mod> ModsRequiredFor { get; set; }
        public bool IsNsfw { get; set; } = false;
        
        // Statistics
        public int TotalDownloads => Versions.Select(v => v.TotalDownloads).Sum();
        public DateTime PublishedTime { get; } = DateTime.Now;
        public DateTime LastUpdated { get; set; }
        
        // Additional Info
        public string? DonationLink { get; set; }
        public string? DiscordLink { get; set; }
        public string? WikiLink { get; set; }
    }
}