using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.APi.Models.DTO;
using NZwalks.APi.Repository;

namespace NZwalks.APi.Controllers
{
    [ApiController]

    [Route("[controller]")]
    public class WalksController : Controller
    {
        private readonly IWalkRepo walkRepo;
        private readonly IMapper mapper;
        private readonly IRegionRepo regionRepo;
        private readonly Iwalkdifficulty walkdifficulty;

        public WalksController(IWalkRepo walkRepo, IMapper mapper, IRegionRepo regionRepo, Iwalkdifficulty walkdifficulty)
        {
            this.walkRepo = walkRepo;
            this.mapper = mapper;
            this.regionRepo = regionRepo;
            this.walkdifficulty = walkdifficulty;
        }
        [HttpGet]
        public async Task<IActionResult> GetWalksAsync()
        {
            // fetch data from db
            var Walksres = await walkRepo.GetAllWalksAsync();
            //convert domain walks to DTO
            var WalksDTO = mapper.Map<List<Models.DTO.Walk>>(Walksres);
            //return response
            return Ok(WalksDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkbyIdAsync")]

        public async Task<IActionResult> GetWalkbyIdAsync(Guid id)
        {
            //get walk from domain
            var Walkdomain = await walkRepo.GetWalkIdAsync(id);
            //convert to DTO
            var WalksDTO = mapper.Map<Models.DTO.Walk>(Walkdomain);
            //return responsE


            return Ok(WalksDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddWAsync([FromBody] Models.DTO.AddWalkReq addWalkReq)
        {
            //validate data
            if (!(await ValidateAddWAsync(addWalkReq)))
            {
                return BadRequest(ModelState);
            }

            //Convert DTo to domain
            var WalkDomain = new Models.Domain.Walk
            {
                Length = addWalkReq.Length,
                Name = addWalkReq.Name,
                RegionId = addWalkReq.RegionId,
                WalkDifficultyId = addWalkReq.WalkDifficultyId,

            };

            //pass domain object to repository to persist this
            WalkDomain = await walkRepo.AddAsync(WalkDomain);
            //convert to DTO
            var WalkDTO = new Models.DTO.Walk
            {
                Id = WalkDomain.Id,
                Length = WalkDomain.Length,
                Name = WalkDomain.Name,
                RegionId = WalkDomain.RegionId,
                WalkDifficultyId = WalkDomain.WalkDifficultyId,
            };
            //response
            return CreatedAtAction(nameof(GetWalkbyIdAsync), new { id = WalkDTO.Id }, WalkDTO);
        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatewalkAsync([FromRoute] Guid id, [FromBody] Models.DTO.Updatewalkreq updatewalkreq)
        {
            //Validate check
            if (!(await ValidateUpdatewalkAsync(updatewalkreq)))
            { return BadRequest(ModelState); }
            //convert DTo to domain
            var WalkDomain = new Models.Domain.Walk
            {
                Length = updatewalkreq.Length,
                Name = updatewalkreq.Name,
                RegionId = updatewalkreq.RegionId,
                WalkDifficultyId = updatewalkreq.WalkDifficultyId,

            };

            ////pass details to repository --get domain object in response 
            WalkDomain = await walkRepo.UpdateAsync(id, WalkDomain);
            //handle null
            if (WalkDomain == null)
            {
                return NotFound("Walk  not found");
            }

            //convert back to dto
            var WalkDTO = new Models.DTO.Walk
            {
                Id = WalkDomain.Id,
                Length = WalkDomain.Length,
                Name = WalkDomain.Name,
                RegionId = WalkDomain.RegionId,
                WalkDifficultyId = WalkDomain.WalkDifficultyId,
            };
            //return response
            return Ok(WalkDTO);

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletewalkAsync(Guid id)

        {
            //repo call to delete
            var walkDomain = await walkRepo.DelWalkAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            var walkDTO = mapper.Map<Models.DTO.Walk>(walkDomain);
            return Ok(walkDTO);

        }
        #region Private methods
        private async Task<bool> ValidateAddWAsync(Models.DTO.AddWalkReq addWalkReq)
        {
            if (addWalkReq == null)
            {
                ModelState.AddModelError(nameof(addWalkReq), $"{nameof(addWalkReq)} can't be empty");
                return false;
            }
            if (String.IsNullOrWhiteSpace(addWalkReq.Name))
            {
                ModelState.AddModelError(nameof(addWalkReq.Name), $"{nameof(addWalkReq.Name)} is reqd");

            }
            if (addWalkReq.Length <= 0)
            {
                ModelState.AddModelError(nameof(addWalkReq.Length), $"{nameof(addWalkReq.Length)} can't be less than zero");
            }

            var region = await regionRepo.GetRegAsync(addWalkReq.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(addWalkReq.RegionId), $"{nameof(addWalkReq.RegionId)} is invalid");
            }
            var WalkDiff = await walkdifficulty.GetAsyncId(addWalkReq.WalkDifficultyId);
            if (WalkDiff == null)
            {
                ModelState.AddModelError(nameof(addWalkReq.WalkDifficultyId), $"{nameof(addWalkReq.WalkDifficultyId)} is invalid");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }
            return true;
        }

        private async Task<bool> ValidateUpdatewalkAsync(Models.DTO.Updatewalkreq updatewalkreq)
        {
            if (updatewalkreq == null)
            {
                ModelState.AddModelError(nameof(updatewalkreq), $"{nameof(updatewalkreq)} can't be empty");
                return false;
            }
            if (String.IsNullOrWhiteSpace(updatewalkreq.Name))
            {
                ModelState.AddModelError(nameof(updatewalkreq.Name), $"{nameof(updatewalkreq.Name)} is reqd");

            }
            if (updatewalkreq.Length <= 0)
            {
                ModelState.AddModelError(nameof(updatewalkreq.Length), $"{nameof(updatewalkreq.Length)} can't be less than zero");
            }

            var region = await regionRepo.GetRegAsync(updatewalkreq.RegionId);
            if (region == null)
            {
                ModelState.AddModelError(nameof(updatewalkreq.RegionId), $"{nameof(updatewalkreq.RegionId)} is invalid");
            }
            var WalkDiff = await walkdifficulty.GetAsyncId(updatewalkreq.WalkDifficultyId);
            if (WalkDiff == null)
            {
                ModelState.AddModelError(nameof(updatewalkreq.WalkDifficultyId), $"{nameof(updatewalkreq.WalkDifficultyId)} is invalid");
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
