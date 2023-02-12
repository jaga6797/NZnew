using NZwalks.APi.Models.Domain;

namespace NZwalks.APi.Repository
{
    public interface IRegionRepo
    {
       Task< IEnumerable<Region>> GetallAsync();

        Task<Region> GetRegAsync(Guid id);

        Task<Region> AddAsync(Region region);

        Task<Region> DelRegAsync(Guid id);

        Task<Region> UpdateAsync(Guid id, Region region);
    }
}
