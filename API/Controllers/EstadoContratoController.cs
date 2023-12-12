using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin,Manager,Employee")]
    public class EstadoContratoController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public EstadoContratoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<EstadoContratoDto>>> Get(
            [FromQuery] Params EstadoContratoParams
        )
        {
            if (EstadoContratoParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.EstadoContratos.GetAllAsync(
                EstadoContratoParams.PageIndex,
                EstadoContratoParams.PageSize
            );
            var EstadoContratoListDto = _mapper.Map<List<EstadoContratoDto>>(registers);
            return new Pager<EstadoContratoDto>(
                EstadoContratoListDto,
                totalRegisters,
                EstadoContratoParams.PageIndex,
                EstadoContratoParams.PageSize
            );
        }
        
        private ActionResult<Pager<EstadoContratoDto>> BadRequest(ApiResponse apiResponse)
        {
        throw new NotImplementedException();
        }
        

        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<EstadoContrato>> Post(EstadoContratoDto EstadoContratoDto)
        {
            var EstadoContrato = _mapper.Map<EstadoContrato>(EstadoContratoDto);
            _unitOfWork.EstadoContratos.Add(EstadoContrato);
            await _unitOfWork.SaveAsync();
            EstadoContratoDto.Id = EstadoContrato.Id;
            return CreatedAtAction(nameof(Post), new { id = EstadoContratoDto.Id }, EstadoContratoDto);
        }
        
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<EstadoContratoDto>> Put(
            int id,
            [FromBody] EstadoContratoDto EstadoContratoDto
        )
        {
            if (EstadoContratoDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var EstadoContrato = _mapper.Map<EstadoContrato>(EstadoContratoDto);
            _unitOfWork.EstadoContratos.Update(EstadoContrato);
            await _unitOfWork.SaveAsync();
            return EstadoContratoDto;
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var EstadoContrato = await _unitOfWork.EstadoContratos.GetByIdAsync(id);
            _unitOfWork.EstadoContratos.Remove(EstadoContrato);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}