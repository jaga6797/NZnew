using Microsoft.EntityFrameworkCore;
using NZwalks.APi.Data;
using NZwalks.APi.Models.Domain;

namespace NZwalks.APi.Repository
{
    public class RegionRepo : IRegionRepo
    {
        private readonly NzWalksDbContext nzWalksDbContext;

        public RegionRepo ( NzWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }
        public async Task< IEnumerable<Region>> GetallAsync()
        {
          return  await nzWalksDbContext.Regions.ToListAsync();
        }
    }
}
