using NZwalks.APi.Models.Domain;

namespace NZwalks.APi.Repository
{
    public interface IRegionRepo
    {
       Task< IEnumerable<Region>> GetallAsync();
    }
}
