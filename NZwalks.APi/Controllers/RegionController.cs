using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.APi.Models.Domain;
using NZwalks.APi.Models.DTO;
using NZwalks.APi.Repository;
using System.Runtime.InteropServices;

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
        public async Task<IActionResult> GetAllRegionsAsync()
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

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetRegionAsync")]
        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
         var region=  await regrep.GetRegAsync(id);
            if(region == null)
            {
                return NotFound();
            };
          var regionDTO=  mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]
        public  async Task<IActionResult> AddRegAsync(Models.DTO.AddRegReq addRegReq)
        {
            //Req (DTO) to Domain model
            var region = new Models.Domain.Region()
            {
                Code = addRegReq.Code,
                Name = addRegReq.Name,
                Area = addRegReq.Area,
                Lat = addRegReq.Lat,
                Long = addRegReq.Long,
                Population = addRegReq.Population,
            };
            //Pass details to repository

             region= await regrep.AddAsync(region);

            //Convert back to DTO

            var regionDTO = new Models.DTO.Region()
            {
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };

            return CreatedAtAction(nameof(GetRegionAsync), new {id = regionDTO .Id}, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteRegAsync(Guid id)
        {
            //Get region from db
           var delRegion = await regrep.DelRegAsync(id);

            //if no reg found, not found respone
            if(delRegion == null)
            {
                return NotFound();
            }

            //If we found, we delete and convert back to DTO
            var regionDTO = new Models.DTO.Region
            {
                Code = delRegion.Code,
                Name = delRegion.Name,
                Area = delRegion.Area,
                Lat = delRegion.Lat,
                Long = delRegion.Long,
                Population = delRegion.Population,

            };


            //return Ok Response
            return Ok(regionDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        
        public async Task<IActionResult> UpdateRegionAsync([FromRoute]Guid id , [FromBody] UpdateRegReq updateRegReq)

        {
            //Convert DTO to domain model

            var updregion = new Models.Domain.Region()
            {
                Code = updateRegReq.Code,
                Name = updateRegReq.Name,
                Area = updateRegReq.Area,
                Lat = updateRegReq.Lat,
                Long = updateRegReq.Long,
                Population = updateRegReq.Population,
            };

            //update region using repository
            updregion= await regrep.UpdateAsync(id, updregion);
            //if null  return not found
            if (updregion == null)
            {
                return NotFound();
            }

            //if not null, convert to DTO var regionDTO = new Models.DTO.Region
            var regionDTO = new Models.DTO.Region
            {
                Code = updregion.Code,
                Name = updregion.Name,
                Area = updregion.Area,
                Lat = updregion.Lat,
                Long = updregion.Long,
                Population = updregion.Population,

            };

            //return ok repsonse
            return Ok(regionDTO);

        }
    }
}
