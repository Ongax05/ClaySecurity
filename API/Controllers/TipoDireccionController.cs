using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TipoDireccionController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public TipoDireccionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<TipoDto>>> Get(
            [FromQuery] Params TipoDireccionParams
        )
        {
            if (TipoDireccionParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.TipoDirecciones.GetAllAsync(
                TipoDireccionParams.PageIndex,
                TipoDireccionParams.PageSize
            );
            var TipoDireccionListDto = _mapper.Map<List<TipoDto>>(registers);
            return new Pager<TipoDto>(
                TipoDireccionListDto,
                totalRegisters,
                TipoDireccionParams.PageIndex,
                TipoDireccionParams.PageSize
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
            var registers = await _unitOfWork.TipoDirecciones.GetAllAsync();
            var TipoDireccionListDto = _mapper.Map<List<TipoDto>>(registers);
            return TipoDireccionListDto;
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<TipoDireccion>> Post(TipoDto TipoDto)
        {
            var TipoDireccion = _mapper.Map<TipoDireccion>(TipoDto);
            _unitOfWork.TipoDirecciones.Add(TipoDireccion);
            await _unitOfWork.SaveAsync();
            TipoDto.Id = TipoDireccion.Id;
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
            var TipoDireccion = _mapper.Map<TipoDireccion>(TipoDto);
            _unitOfWork.TipoDirecciones.Update(TipoDireccion);
            await _unitOfWork.SaveAsync();
            return TipoDto;
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var TipoDireccion = await _unitOfWork.TipoDirecciones.GetByIdAsync(id);
            _unitOfWork.TipoDirecciones.Remove(TipoDireccion);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}