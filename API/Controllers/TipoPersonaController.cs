using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TipoPersonaController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public TipoPersonaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<TipoDto>>> Get(
            [FromQuery] Params TipoPersonaParams
        )
        {
            if (TipoPersonaParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.TipoPersonas.GetAllAsync(
                TipoPersonaParams.PageIndex,
                TipoPersonaParams.PageSize
            );
            var TipoPersonaListDto = _mapper.Map<List<TipoDto>>(registers);
            return new Pager<TipoDto>(
                TipoPersonaListDto,
                totalRegisters,
                TipoPersonaParams.PageIndex,
                TipoPersonaParams.PageSize
            );
        }
        
        private ActionResult<Pager<TipoDto>> BadRequest(ApiResponse apiResponse)
        {
        throw new NotImplementedException();
        }
        
        [HttpGet]
        [MapToApiVersion("1.1")]
        public async Task<ActionResult<IEnumerable<TipoDto>>> Get1_1()
        {
            var registers = await _unitOfWork.TipoPersonas.GetAllAsync();
            var TipoPersonaListDto = _mapper.Map<List<TipoDto>>(registers);
            return TipoPersonaListDto;
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<TipoPersona>> Post(TipoDto TipoDto)
        {
            var TipoPersona = _mapper.Map<TipoPersona>(TipoDto);
            _unitOfWork.TipoPersonas.Add(TipoPersona);
            await _unitOfWork.SaveAsync();
            TipoDto.Id = TipoPersona.Id;
            return CreatedAtAction(nameof(Post), new { id = TipoDto.Id }, TipoDto);
        }
        
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<TipoDto>> Put(
            int id,
            [FromBody] TipoDto TipoDto
        )
        {
            if (TipoDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var TipoPersona = _mapper.Map<TipoPersona>(TipoDto);
            _unitOfWork.TipoPersonas.Update(TipoPersona);
            await _unitOfWork.SaveAsync();
            return TipoDto;
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var TipoPersona = await _unitOfWork.TipoPersonas.GetByIdAsync(id);
            _unitOfWork.TipoPersonas.Remove(TipoPersona);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }        
    }
}