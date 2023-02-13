using NZwalks.APi.Models.Domain;

namespace NZwalks.APi.Repository
{
    public interface IWalkRepo
    {
        Task<IEnumerable<Walk>> GetAllWalksAsync();

        Task<Walk> GetWalkIdAsync(Guid id);

        Task<Walk> AddAsync(Walk walk);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task<Walk> DelWalkAsync(Guid id);
    }
}
