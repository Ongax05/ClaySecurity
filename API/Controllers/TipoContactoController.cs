using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TipoContactoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public TipoContactoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<TipoDto>>> Get(
            [FromQuery] Params TipoContactoParams
        )
        {
            if (TipoContactoParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.TipoContactos.GetAllAsync(
                TipoContactoParams.PageIndex,
                TipoContactoParams.PageSize
            );
            var TipoContactoListDto = _mapper.Map<List<TipoDto>>(registers);
            return new Pager<TipoDto>(
                TipoContactoListDto,
                totalRegisters,
                TipoContactoParams.PageIndex,
                TipoContactoParams.PageSize
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
            var registers = await _unitOfWork.TipoContactos.GetAllAsync();
            var TipoContactoListDto = _mapper.Map<List<TipoDto>>(registers);
            return TipoContactoListDto;
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<TipoContacto>> Post(TipoDto TipoDto)
        {
            var TipoContacto = _mapper.Map<TipoContacto>(TipoDto);
            _unitOfWork.TipoContactos.Add(TipoContacto);
            await _unitOfWork.SaveAsync();
            TipoDto.Id = TipoContacto.Id;
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
            var TipoContacto = _mapper.Map<TipoContacto>(TipoDto);
            _unitOfWork.TipoContactos.Update(TipoContacto);
            await _unitOfWork.SaveAsync();
            return TipoDto;
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var TipoContacto = await _unitOfWork.TipoContactos.GetByIdAsync(id);
            _unitOfWork.TipoContactos.Remove(TipoContacto);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}