using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NZwalks.APi.Data;
using NZwalks.APi.Models.Domain;

namespace NZwalks.APi.Repository
{
    public class Walkdifficultyrepo : Iwalkdifficulty
    {
        private readonly NzWalksDbContext nzWalksDbContext;


        public Walkdifficultyrepo(NzWalksDbContext nzWalksDbContext)
        {
            this.nzWalksDbContext = nzWalksDbContext;

        }

        public async Task<WalkDifficulty> AddASync(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            await nzWalksDbContext.WalkDifficulties.AddAsync(walkDifficulty);
            await nzWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

       
        public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
        {
            return await nzWalksDbContext.WalkDifficulties.ToListAsync();
        }

        public async Task<WalkDifficulty> GetAsyncId(Guid id)
        {
            return await nzWalksDbContext.WalkDifficulties.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WalkDifficulty> UpdwdASync(Guid id, WalkDifficulty WalkDifficulty)
        {
            var existwd = await nzWalksDbContext.WalkDifficulties.FindAsync(id);
            if (existwd == null)
            {
                return null;

            }
            existwd.Code = WalkDifficulty.Code;
            await nzWalksDbContext.SaveChangesAsync();
            return existwd;
        }
        public async Task<WalkDifficulty> DelAsync(Guid id)
        {
            var existwd = await nzWalksDbContext.WalkDifficulties.FindAsync(id);
            if (existwd != null)
            {
                nzWalksDbContext.WalkDifficulties.Remove(existwd);
                await nzWalksDbContext.SaveChangesAsync();
                return existwd;
            }return null;
        }
    }
}
