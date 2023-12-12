using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin,Manager,Employee")]
    public class TurnoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public TurnoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<TurnoDto>>> Get(
            [FromQuery] Params TurnoParams
        )
        {
            if (TurnoParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.Turnos.GetAllAsync(
                TurnoParams.PageIndex,
                TurnoParams.PageSize
            );
            var TurnoListDto = _mapper.Map<List<TurnoDto>>(registers);
            return new Pager<TurnoDto>(
                TurnoListDto,
                totalRegisters,
                TurnoParams.PageIndex,
                TurnoParams.PageSize
            );
        }
        
        private ActionResult<Pager<TurnoDto>> BadRequest(ApiResponse apiResponse)
        {
        throw new NotImplementedException();
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Turno>> Post(TurnoDto TurnoDto)
        {
            var Turno = _mapper.Map<Turno>(TurnoDto);
            _unitOfWork.Turnos.Add(Turno);
            await _unitOfWork.SaveAsync();
            TurnoDto.Id = Turno.Id;
            return CreatedAtAction(nameof(Post), new { id = TurnoDto.Id }, TurnoDto);
        }
        
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<TurnoDto>> Put(
            int id,
            [FromBody] TurnoDto TurnoDto
        )
        {
            if (TurnoDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var Turno = _mapper.Map<Turno>(TurnoDto);
            _unitOfWork.Turnos.Update(Turno);
            await _unitOfWork.SaveAsync();
            return TurnoDto;
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var Turno = await _unitOfWork.Turnos.GetByIdAsync(id);
            _unitOfWork.Turnos.Remove(Turno);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}