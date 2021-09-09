using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.Triggered;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;
using Version = XIVRepo.Core.Models.Mods.Version;

namespace XIVRepo.EntityFramework.Triggers
{
    public class ModVersionTrigger : IAfterSaveTrigger<Version>
    {
        private readonly XivRepoDbContext _dbContext;

        public ModVersionTrigger(XivRepoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AfterSave(ITriggerContext<Version> context, CancellationToken cancellationToken)
        {
            var updatedMod = await _dbContext.Mods
                .Where(m => m.Id == context.Entity.Mod.Id)
                .FirstOrDefaultAsync();
            
            context.Entity.LastUpdated = DateTime.Now;
            updatedMod.LastUpdated = DateTime.Now;
            await _dbContext.SaveChangesAsync();
        }
    }
}