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

        public async Task<Region> GetRegAsync(Guid id)
        {
           return await nzWalksDbContext.Regions.FirstOrDefaultAsync(x =>x.Id == id);
        }
        public async Task<Region> AddAsync(Region region)
        {
            region.Id=new Guid();
            await nzWalksDbContext.AddAsync(region);
            await nzWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DelRegAsync(Guid id)
        {
            var regionrecord = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

              if (regionrecord == null)
            {
                return null;
            }
            nzWalksDbContext.Regions.Remove(regionrecord);
            await nzWalksDbContext.SaveChangesAsync();
            return regionrecord;
        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {
            var existrecord = await nzWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existrecord == null)
            {
                return null;
            }
            existrecord.Code = region.Code;
            existrecord.Name = region.Name;
            existrecord.Area = region.Area;
            existrecord.Lat = region.Lat;
            existrecord.Long = region.Long;
            existrecord.Population= region.Population;

            await nzWalksDbContext.SaveChangesAsync();
            return existrecord;
        }
    }
}
