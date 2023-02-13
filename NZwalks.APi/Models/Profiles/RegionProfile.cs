using AutoMapper;

namespace NZwalks.APi.Models.Profiles
{
    public class RegionProfiile: Profile
    {
        public RegionProfiile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>();
              //if we have two different names(eg. here -> id, regionid) of fields in domain and dto ->then we can map those like this ->  .ForMember(dest => dest.Id, options => options.MapFrom(src => src.RegionId));
            
        }
    }
}
    