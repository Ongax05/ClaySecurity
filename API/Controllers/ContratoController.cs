using API.Dtos;
using API.Helpers;
using Aplication.UnitOfWork;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin,Manager,Employee")]
    public class ContratoController(IUnitOfWork unitOfWork, IMapper mapper) : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<ContratoDto>>> Get(
            [FromQuery] Params ContratoParams
        )
        {
            if (ContratoParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.Contratos.GetAllAsync(
                ContratoParams.PageIndex,
                ContratoParams.PageSize
            );
            var ContratoListDto = _mapper.Map<List<ContratoDto>>(registers);
            return new Pager<ContratoDto>(
                ContratoListDto,
                totalRegisters,
                ContratoParams.PageIndex,
                ContratoParams.PageSize
            );
        }
        
        private ActionResult<Pager<ContratoDto>> BadRequest(ApiResponse apiResponse)
        {
        throw new NotImplementedException();
        }
        
        [HttpGet("GetContratosActivos")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<NumeroDeContratoEInvolucradosDto>>> GetContratosActivos(
            [FromQuery] Params ContratoParams
        )
        {
            var (totalRegisters, registers) = await _unitOfWork.Contratos.GetContratosActivos(
                ContratoParams.PageIndex,
                ContratoParams.PageSize
            );

            List<NumeroDeContratoEInvolucradosDto> Nci = [];

            foreach(var r in registers)
            {
                var nombreEmpleado = await _unitOfWork.Contratos.GetNombreEmpleadoPorIdDeContrato(r.Id);
                var newR = new NumeroDeContratoEInvolucradosDto()
                {
                    ContratoId = r.Id,
                    NombreCliente = r.Cliente.Nombre,
                    NombreEmpleado = nombreEmpleado
                };
                Nci.Add(newR);
            }

            return new Pager<NumeroDeContratoEInvolucradosDto>(
                Nci,
                totalRegisters,
                ContratoParams.PageIndex,
                ContratoParams.PageSize
            );
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Contrato>> Post(ContratoDto ContratoDto)
        {
            var Contrato = _mapper.Map<Contrato>(ContratoDto);
            _unitOfWork.Contratos.Add(Contrato);
            await _unitOfWork.SaveAsync();
            ContratoDto.Id = Contrato.Id;
            return CreatedAtAction(nameof(Post), new { id = ContratoDto.Id }, ContratoDto);
        }
        
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ContratoDto>> Put(
            int id,
            [FromBody] ContratoDto ContratoDto
        )
        {
            if (ContratoDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var Contrato = _mapper.Map<Contrato>(ContratoDto);
            _unitOfWork.Contratos.Update(Contrato);
            await _unitOfWork.SaveAsync();
            return ContratoDto;
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var Contrato = await _unitOfWork.Contratos.GetByIdAsync(id);
            _unitOfWork.Contratos.Remove(Contrato);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}