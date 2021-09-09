using System.Collections.Generic;
using System.Threading.Tasks;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using XIVRepo.Core.Models.Accounts;
using XIVRepo.Core.Models.Files;
using XIVRepo.Core.Models.Mods;

namespace XIVRepo.EntityFramework
{
    public class XivRepoDbContext : DbContext
    {
        public DbSet<Account> UserAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Mod> Mods { get; set; }
        public DbSet<FileUpload> Files { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Version> ModVersions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PreviewImage> ModPreviewImages { get; set; }
        
        public XivRepoDbContext(DbContextOptions<XivRepoDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Indexes and Keys
            modelBuilder.Entity<Mod>()
                .HasIndex(m => m.Slug)
                .IsUnique();
            modelBuilder.Entity<Mod>()
                .Property(m => m.PublishedTime)
                .HasDefaultValueSql("getdate()");
            modelBuilder.Entity<Mod>()
                .Property(m => m.LastUpdated)
                .ValueGeneratedOnUpdate();
            modelBuilder.Entity<ModFollowers>()
                .HasKey(i => new { i.FollowerId, i.ModId });
            modelBuilder.Entity<Tag>()
                .HasKey(t => t.TagId);
            #endregion

            #region  Join Tables
            // User Accounts and Role
            modelBuilder.Entity<Account>()
                .HasMany(p => p.Roles)
                .WithMany(p => p.AccountsWithRole)
                .UsingEntity<Dictionary<string, object>>(
                    "UserAccountRoles",
                    e => e
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId"),
                    e => e
                        .HasOne<Account>()
                        .WithMany()
                        .HasForeignKey("AccountId"));
            
            // Mod Categories
            modelBuilder.Entity<Mod>()
                .HasMany(p => p.Categories)
                .WithMany(p => p.ModsInCategory)
                .UsingEntity<Dictionary<string, object>>(
                    "ModsInCategories",
                    e => e
                        .HasOne<Category>()
                        .WithMany()
                        .HasForeignKey("CategoryId"),
                    e => e
                        .HasOne<Mod>()
                        .WithMany()
                        .HasForeignKey("ModId"));
            
            // Mod Tags
            modelBuilder.Entity<Mod>()
                .HasMany(p => p.UserTags)
                .WithMany(p => p.ModsWithTag)
                .UsingEntity<Dictionary<string, object>>(
                    "ModsWithTags",
                    e => e
                        .HasOne<Tag>()
                        .WithMany()
                        .HasForeignKey("TagId"),
                    e => e
                        .HasOne<Mod>()
                        .WithMany()
                        .HasForeignKey("ModId"));
            
            // Mod Dependencies 
            modelBuilder.Entity<Mod>()
                .HasMany(m => m.ModDependencies)
                .WithMany(m => m.ModsRequiredFor)
                .UsingEntity<Dictionary<string, object>>(
                    "ModDependencies",
                    e => e
                        .HasOne<Mod>()
                        .WithMany()
                        .HasForeignKey("ModDependencyId"),
                    e => e
                        .HasOne<Mod>()
                        .WithMany()
                        .HasForeignKey("BaseModId"));
            #endregion
        }
    }
}