using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin,Manager,Employee")]
    public class DireccionPersonaController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public DireccionPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<DireccionPersonaDto>>> Get(
            [FromQuery] Params DireccionPersonaParams
        )
        {
            if (DireccionPersonaParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.DireccionPersonas.GetAllAsync(
                DireccionPersonaParams.PageIndex,
                DireccionPersonaParams.PageSize
            );
            var DireccionPersonaListDto = _mapper.Map<List<DireccionPersonaDto>>(registers);
            return new Pager<DireccionPersonaDto>(
                DireccionPersonaListDto,
                totalRegisters,
                DireccionPersonaParams.PageIndex,
                DireccionPersonaParams.PageSize
            );
        }
        
        private ActionResult<Pager<DireccionPersonaDto>> BadRequest(ApiResponse apiResponse)
        {
        throw new NotImplementedException();
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<DireccionPersona>> Post(DireccionPersonaDto DireccionPersonaDto)
        {
            var DireccionPersona = _mapper.Map<DireccionPersona>(DireccionPersonaDto);
            _unitOfWork.DireccionPersonas.Add(DireccionPersona);
            await _unitOfWork.SaveAsync();
            DireccionPersonaDto.Id = DireccionPersona.Id;
            return CreatedAtAction(nameof(Post), new { id = DireccionPersonaDto.Id }, DireccionPersonaDto);
        }
        
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<DireccionPersonaDto>> Put(
            int id,
            [FromBody] DireccionPersonaDto DireccionPersonaDto
        )
        {
            if (DireccionPersonaDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var DireccionPersona = _mapper.Map<DireccionPersona>(DireccionPersonaDto);
            _unitOfWork.DireccionPersonas.Update(DireccionPersona);
            await _unitOfWork.SaveAsync();
            return DireccionPersonaDto;
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var DireccionPersona = await _unitOfWork.DireccionPersonas.GetByIdAsync(id);
            _unitOfWork.DireccionPersonas.Remove(DireccionPersona);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}