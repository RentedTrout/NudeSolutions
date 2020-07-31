using Microsoft.EntityFrameworkCore;

namespace NudeSolutions.Data
{
    public class NudeSolutionsContext : DbContext
    {
        public NudeSolutionsContext(DbContextOptions<NudeSolutionsContext> options)
            : base(options)
        {
        }

        public DbSet<Models.InsuranceItem> InsuranceItem { get; set; }

        public DbSet<Models.InsuranceCategory> InsuranceCategory { get; set; }
    }
}