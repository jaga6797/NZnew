using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.APi.Models.Domain;
using NZwalks.APi.Models.DTO;
using NZwalks.APi.Repository;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace NZwalks.APi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // or [Route("Region")]
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
            var region = await regrep.GetRegAsync(id);
            if (region == null)
            {
                return NotFound();
            };
            var regionDTO = mapper.Map<Models.DTO.Region>(region);
            return Ok(regionDTO);
        }

        [HttpPost]


        public async Task<IActionResult> AddRegAsync(Models.DTO.AddRegReq addRegReq)
        {
            ////validate the req

            //if (!ValidateAddRegAsync(addRegReq))
            //{
            //    return BadRequest(ModelState);
            //}


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

            region = await regrep.AddAsync(region);

            //Convert back to DTO

            var regionDTO = new Models.DTO.Region()
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,
            };

            return CreatedAtAction(nameof(GetRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteRegAsync(Guid id)
        {
            //Get region from db
            var delRegion = await regrep.DelRegAsync(id);

            //if no reg found, not found respone
            if (delRegion == null)
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

        public async Task<IActionResult> UpdateRegionAsync([FromRoute] Guid id, [FromBody] UpdateRegReq updateRegReq)

        { //validate req
            //if(!ValidateUpdateRegionAsync(updateRegReq))
            //{
            //    return BadRequest(ModelState);
            //}
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
            updregion = await regrep.UpdateAsync(id, updregion);
            //if null  return not found
            if (updregion == null)
            {
                return NotFound();
            }

            //if not null, convert to DTO var regionDTO = new Models.DTO.Region
            var regionDTO = new Models.DTO.Region
            {
                Id = updregion.Id,
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

        #region Private Methods
        private bool ValidateAddRegAsync(AddRegReq addRegReq)
        {
            if (addRegReq == null)
            {
                ModelState.AddModelError(nameof(addRegReq), "$ Reg req data required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(addRegReq.Code))
            {
                ModelState.AddModelError(nameof(addRegReq.Code), $"{nameof(addRegReq.Code)} cannot be null or whitespace or empty");
            }
            if (string.IsNullOrWhiteSpace(addRegReq.Name))
            {
                ModelState.AddModelError(nameof(addRegReq.Name), $"{nameof(addRegReq.Name)} cannot be null or whitespace or empty");
            }
            if (addRegReq.Area <= 0)
            {
                ModelState.AddModelError(nameof(addRegReq.Area), $"{nameof(addRegReq.Area)} cannot be less than or equal to zero");
            }
            if (addRegReq.Lat <= 0)
            {
                ModelState.AddModelError(nameof(addRegReq.Lat), $"{nameof(addRegReq.Lat)} cannot be less than or equal to zero");
            }
            if (addRegReq.Long <= 0)
            {
                ModelState.AddModelError(nameof(addRegReq.Long), $"{nameof(addRegReq.Long)} cannot be less than or equal to zero");
            }
            if (addRegReq.Population < 0)
            {
                ModelState.AddModelError(nameof(addRegReq.Population), $"{nameof(addRegReq.Population)} cannot be less than or equal to zero");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }
        private bool ValidateUpdateRegionAsync(UpdateRegReq updateRegReq)
        {
            if (updateRegReq == null)
            {
                ModelState.AddModelError(nameof(updateRegReq), "$ Reg req data required");
                return false;
            }

            if (string.IsNullOrWhiteSpace(updateRegReq.Code))
            {
                ModelState.AddModelError(nameof(updateRegReq.Code), $"{nameof(updateRegReq.Code)} cannot be null or whitespace or empty");
            }
            if (string.IsNullOrWhiteSpace(updateRegReq.Name))
            {
                ModelState.AddModelError(nameof(updateRegReq.Name), $"{nameof(updateRegReq.Name)} cannot be null or whitespace or empty");
            }
            if (updateRegReq.Area <= 0)
            {
                ModelState.AddModelError(nameof(updateRegReq.Area), $"{nameof(updateRegReq.Area)} cannot be less than or equal to zero");
            }       
            if (updateRegReq.Population< 0)
            {
                ModelState.AddModelError(nameof(updateRegReq.Population), $"{nameof(updateRegReq.Population)} cannot be less than or equal to zero");
            }

            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

  

        #endregion


    }



}
