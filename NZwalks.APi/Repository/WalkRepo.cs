using Microsoft.EntityFrameworkCore;
using NZwalks.APi.Data;
using NZwalks.APi.Models.Domain;

namespace NZwalks.APi.Repository
{
    public class WalkRepo : IWalkRepo
    {
        private readonly NzWalksDbContext nzWalksDbContext;

        public WalkRepo(NzWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;
        }



        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            return await nzWalksDbContext.Walks
                 .Include(x => x.region)
                 .Include(x => x.WalkDifficulty)
                 .ToListAsync();

        }

        public async Task<Walk> GetWalkIdAsync(Guid id)
        {
            return await nzWalksDbContext.Walks
            .Include(x => x.region)
            .Include(x => x.WalkDifficulty)
            .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Walk> AddAsync(Walk walk)
        {
            //assign new id
            walk.Id = Guid.NewGuid();
            await nzWalksDbContext.Walks.AddAsync(walk);
          await  nzWalksDbContext.SaveChangesAsync();
            return walk;

        }

        public async Task<Walk> UpdateAsync(Guid id, Walk walk)
        {
            var existwalk = await nzWalksDbContext.Walks.FindAsync(id);
            if (existwalk != null)
            {

                existwalk.Length = walk.Length;
                existwalk.Name = walk.Name;
                existwalk.WalkDifficultyId = walk.WalkDifficultyId;
                existwalk.RegionId = walk.RegionId;
                await nzWalksDbContext.SaveChangesAsync();
                return existwalk;

            }
            return null;
        }

        public async Task<Walk> DelWalkAsync(Guid id)
        {

            var existwalk = await nzWalksDbContext.Walks.FindAsync(id);
            if (existwalk == null)
            {
                return null;
            }
            nzWalksDbContext.Walks.Remove(existwalk);
            await nzWalksDbContext.SaveChangesAsync();
            return existwalk;
        }
    }
}
