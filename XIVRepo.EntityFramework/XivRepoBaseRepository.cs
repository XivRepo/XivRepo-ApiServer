namespace XIVRepo.EntityFramework
{
    public class XivRepoBaseRepository
    {
        internal readonly XivRepoDbContext _context;

        public XivRepoBaseRepository(XivRepoDbContext dbContext)
        {
            _context = dbContext;
        }
    }
}