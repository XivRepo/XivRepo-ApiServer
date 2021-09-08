namespace XIVRepo.EntityFramework
{
    public class XivRepoBaseRepository
    {
        protected readonly XivRepoDbContext Context;

        public XivRepoBaseRepository(XivRepoDbContext dbContext)
        {
            Context = dbContext;
        }
    }
}