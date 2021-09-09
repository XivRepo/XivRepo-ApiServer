
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using XIVRepo.Core.Models.Files;

namespace XIVRepo.Core.Models.Mods
{
    public class Version
    {
        // Basic Info
        [Key]
        public Guid Id { get; set; }
        [Column("VersionModId")]
        public Mod Mod { get; set; }
        public VersionType ReleaseType { get; set; }
        public bool IsMainVersion { get; set; }
        public string VersionNumber { get; set; }
        public string? Changelog { get; set; }
        public string? ExternalUrl { get; set; }
        [Column("FileId")]
        public FileUpload VersionFile { get; set; }
        
        // Statistics
        public int TotalDownloads { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime PublishedTime { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime LastUpdated { get; set; }
    }
}