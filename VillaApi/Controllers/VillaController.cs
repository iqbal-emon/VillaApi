using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VillaApi.Data;
using VillaApi.Model;
using VillaApi.Model.Dtos;
using VillaApi.Utility;

namespace VillaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        public VillaController(AppDbContext appDbContext,IMapper mapper) {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public ActionResult<List<VillaResponseDto>> GetVillas()
        {
            var villaList = _appDbContext.Villas
                .Select(v => new VillaResponseDto
                {
                    Id = v.Id,
                    Name = v.Name
                })
                .ToList();

            return Ok(villaList);
        }
        [Authorize(Roles ="admin")]
        [HttpGet("{id:int}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public ActionResult<VillaResponseDto> GetByIdVilla(int id)
        {
            if (id < 0)
            {
                return BadRequest("Invalid ID provided.");
            }
            var villList = _appDbContext.Villas.FirstOrDefault(v => v.Id == id);
            if (villList == null)
            {
                return NotFound($"Villa with ID {id} not found.");
            }
            return Ok(villList);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult<VillaRequestDto> CreateVilla(VillaRequestDto villa)
        {
            if (villa == null)
            {
                return BadRequest("Invalid Request");
            }

            // Do not set Id, let the database handle it
            var newVilla = new Villa
            {
                Name = villa.Name
            };

            _appDbContext.Villas.Add(newVilla);
            _appDbContext.SaveChanges();

           
            return Ok(_mapper.Map<VillaResponseDto>(newVilla));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateVilla(int id, VillaRequestDto villaDto)
        {
            if (villaDto == null || id <= 0)
            {
                return BadRequest("Invalid data.");
            }

            var villa = _appDbContext.Villas.AsNoTracking().FirstOrDefault(v => v.Id == id);
            if (villa == null)
            {
                return NotFound($"Villa with ID {id} not found.");
            }


            villa.Name = villaDto.Name;
            
            _appDbContext.Villas.Update(villa);
            _appDbContext.SaveChanges();
            var response = new VillaResponseDto
            {
                Id = villa.Id,
                Name = villa.Name
            };


            return Ok(response);
        }
    }
}
