using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.APi.Models.Domain;
using NZwalks.APi.Repository;

namespace NZwalks.APi.Controllers
{
    [ApiController]  
    [Route("controller")]
    // or [Route("Regions")]
    public class RegionController : Controller
    {
        private readonly IRegionRepo regrep;
        private readonly IMapper mapper;

        public RegionController(IRegionRepo regrep, IMapper mapper)
        {
            this.regrep = regrep;
            this.mapper = mapper;
        }
        [HttpGet]

        //using Domain model
        //public IActionResult GetAllRegions() 
        //{
        //   var reg= regrep.Getall();

        //    return Ok(reg);
        //}

        //using DTO-- returns only Dto regions
        public async Task<IActionResult> GetAllRegions()
        {


            var reg = await regrep.GetallAsync();

            //var regDTO = new List<Models.DTO.Region>();

            //reg.ToList().ForEach(region =>
            //{

            //    var regionDTO = new Models.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Area = region.Area,
            //        Population = region.Population,

            //    };
            //    regDTO.Add(regionDTO);
            //});

            //same using mapper we can return DTO regions
            var regDTO = mapper.Map<List<Models.DTO.Region>>(reg);
            
            return Ok(regDTO);
        }
    }
}
