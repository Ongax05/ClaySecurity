using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin,Manager,Employee")]
    public class CategoriaPersonaController(IUnitOfWork unitOfWork, IMapper mapper) : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<CategoriaPersonaDto>>> Get(
            [FromQuery] Params CategoriaPersonaParams
        )
        {
            if (CategoriaPersonaParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.CategoriaPersonas.GetAllAsync(
                CategoriaPersonaParams.PageIndex,
                CategoriaPersonaParams.PageSize
            );
            var CategoriaPersonaListDto = _mapper.Map<List<CategoriaPersonaDto>>(registers);
            return new Pager<CategoriaPersonaDto>(
                CategoriaPersonaListDto,
                totalRegisters,
                CategoriaPersonaParams.PageIndex,
                CategoriaPersonaParams.PageSize
            );
        }
        
        private ActionResult<Pager<CategoriaPersonaDto>> BadRequest(ApiResponse apiResponse)
        {
        throw new NotImplementedException();
        }
        
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CategoriaPersona>> Post(CategoriaPersonaDto CategoriaPersonaDto)
        {
            var CategoriaPersona = _mapper.Map<CategoriaPersona>(CategoriaPersonaDto);
            _unitOfWork.CategoriaPersonas.Add(CategoriaPersona);
            await _unitOfWork.SaveAsync();
            CategoriaPersonaDto.Id = CategoriaPersona.Id;
            return CreatedAtAction(nameof(Post), new { id = CategoriaPersonaDto.Id }, CategoriaPersonaDto);
        }
        
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CategoriaPersonaDto>> Put(
            int id,
            [FromBody] CategoriaPersonaDto CategoriaPersonaDto
        )
        {
            if (CategoriaPersonaDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var CategoriaPersona = _mapper.Map<CategoriaPersona>(CategoriaPersonaDto);
            _unitOfWork.CategoriaPersonas.Update(CategoriaPersona);
            await _unitOfWork.SaveAsync();
            return CategoriaPersonaDto;
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var CategoriaPersona = await _unitOfWork.CategoriaPersonas.GetByIdAsync(id);
            _unitOfWork.CategoriaPersonas.Remove(CategoriaPersona);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}