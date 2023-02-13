using NZwalks.APi.Models.Domain;

namespace NZwalks.APi.Repository
{
    public interface Iwalkdifficulty
    {
        Task<IEnumerable<WalkDifficulty>> GetAllAsync();
        Task<WalkDifficulty> GetAsyncId(Guid id);

        Task<WalkDifficulty> AddASync(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> UpdwdASync(Guid id, WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> DelAsync(Guid id);



    }
}
