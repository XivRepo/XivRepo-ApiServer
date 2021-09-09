
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XIVRepo.Core.Models.Files;

namespace XIVRepo.Core.Models.Mods
{
    public class Version
    {
        // Basic Info
        public Guid Id { get; set; }
        public Mod Mod { get; set; }
        public VersionType ReleaseType { get; set; }
        public bool IsMainVersion { get; set; }
        public string VersionNumber { get; set; }
        public string? Changelog { get; set; }
        public string? ExternalUrl { get; set; }
        public FileUpload VersionFile { get; set; }
        
        // Statistics
        public int TotalDownloads { get; set; }
        public DateTime PublishedTime { get; } = DateTime.Now;
        public DateTime LastUpdated { get; set; }
    }
}