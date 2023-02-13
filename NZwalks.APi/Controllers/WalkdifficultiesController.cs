using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZwalks.APi.Models.DTO;
using NZwalks.APi.Repository;

namespace NZwalks.APi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WalkdifficultiesController : Controller
    {
        private readonly Iwalkdifficulty walkdifficulty;
        private readonly IMapper mapper;

        public WalkdifficultiesController(Iwalkdifficulty walkdifficulty, IMapper mapper)
        {
            this.walkdifficulty = walkdifficulty;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllWd()
        {

            var wddomain = await walkdifficulty.GetAllAsync();
            //Convert domain to DTO

            var wdDTO = mapper.Map<List<Models.DTO.WalkDifficulty>>(wddomain);
            return Ok(wdDTO);
        }
        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetIdAsync")]
        public async Task<IActionResult> GetIdAsync(Guid id)
        {

            var wd = await walkdifficulty.GetAsyncId(id);
            if (wd == null)
            {
                return NotFound();
            } //Convert domain to DTO
            var wdDTO = mapper.Map<Models.DTO.WalkDifficulty>(wd);
            return Ok(wdDTO);
        }
        [HttpPost]

        public async Task<IActionResult> AddWDAsync(AddWDReq addWDReq)
        {
            //convert dto to domain
            var wddomain = new Models.Domain.WalkDifficulty()
            {
                Code = addWDReq.Code
            };
            //call repo

            wddomain = await walkdifficulty.AddASync(wddomain);
            //convert to DTO

            var WDDTO = mapper.Map<Models.DTO.WalkDifficulty>(wddomain);
            return CreatedAtAction(nameof(GetIdAsync), new { id = wddomain.Id }, wddomain);

        }
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateWD(Guid id, UPdWDReq uPdWDReq)
        {
            //convert dto domain
            var wddomain = new Models.Domain.WalkDifficulty
            {
                Code = uPdWDReq.Code
            };
            //call repo to update
            wddomain = await walkdifficulty.UpdwdASync(id, wddomain);

            if(wddomain==null) { return NotFound(); }
            //convert to DTO
            var WDDTO = mapper.Map<Models.DTO.WalkDifficulty>(wddomain);
            return Ok(WDDTO);

        }
        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> WdDelAsync(Guid id)
        {
         var Wddomain=  await walkdifficulty.DelAsync(id);
           if(Wddomain == null) {
                return NotFound();

            }
            var WDDTO = mapper.Map<Models.DTO.WalkDifficulty>(Wddomain);
            return Ok(WDDTO);
        }
    }
}
