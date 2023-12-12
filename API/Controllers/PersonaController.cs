using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin,Manager,Employee")]
    public class PersonaController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PersonaDto>>> Get([FromQuery] Params PersonaParams)
        {
            if (PersonaParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork
                .Personas
                .GetAllAsync(PersonaParams.PageIndex, PersonaParams.PageSize);
            var PersonaListDto = _mapper.Map<List<PersonaDto>>(registers);
            return new Pager<PersonaDto>(
                PersonaListDto,
                totalRegisters,
                PersonaParams.PageIndex,
                PersonaParams.PageSize
            );
        }

        [HttpGet("GetBumangueses")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PersonaDto>>> GetBumangueses(
            [FromQuery] Params PersonaParams
        )
        {
            if (PersonaParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork
                .Personas
                .GetBumangueses(PersonaParams.PageIndex, PersonaParams.PageSize);
            var PersonaListDto = _mapper.Map<List<PersonaDto>>(registers);
            return new Pager<PersonaDto>(
                PersonaListDto,
                totalRegisters,
                PersonaParams.PageIndex,
                PersonaParams.PageSize
            );
        }

        [HttpGet("GetEmpleados")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PersonaDto>>> GetEmpleados(
            [FromQuery] Params PersonaParams
        )
        {
            if (PersonaParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork
                .Personas
                .GetEmpleados(PersonaParams.PageIndex, PersonaParams.PageSize);
            var PersonaListDto = _mapper.Map<List<PersonaDto>>(registers);
            return new Pager<PersonaDto>(
                PersonaListDto,
                totalRegisters,
                PersonaParams.PageIndex,
                PersonaParams.PageSize
            );
        }

        [HttpGet("GetEmpleadosCon5AñosDeAntiguedad")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PersonaDto>>> GetEmpleadosCon5AñosDeAntiguedad(
            [FromQuery] Params PersonaParams
        )
        {
            if (PersonaParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork
                .Personas
                .GetClientesCon5AñosDeAntiguedad(PersonaParams.PageIndex, PersonaParams.PageSize);
            var PersonaListDto = _mapper.Map<List<PersonaDto>>(registers);
            return new Pager<PersonaDto>(
                PersonaListDto,
                totalRegisters,
                PersonaParams.PageIndex,
                PersonaParams.PageSize
            );
        }

        [HttpGet("GetEmpleadosDePiedecuestaYGiron")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PersonaDto>>> GetEmpleadosDePiedecuestaYGiron(
            [FromQuery] Params PersonaParams
        )
        {
            if (PersonaParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork
                .Personas
                .GetEmpleadosDePiedecuestaYGiron(PersonaParams.PageIndex, PersonaParams.PageSize);
            var PersonaListDto = _mapper.Map<List<PersonaDto>>(registers);
            return new Pager<PersonaDto>(
                PersonaListDto,
                totalRegisters,
                PersonaParams.PageIndex,
                PersonaParams.PageSize
            );
        }

        [HttpGet("GetVigilantes")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<PersonaDto>>> GetVigilantes(
            [FromQuery] Params PersonaParams
        )
        {
            if (PersonaParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork
                .Personas
                .GetVigilantes(PersonaParams.PageIndex, PersonaParams.PageSize);
            var PersonaListDto = _mapper.Map<List<PersonaDto>>(registers);
            return new Pager<PersonaDto>(
                PersonaListDto,
                totalRegisters,
                PersonaParams.PageIndex,
                PersonaParams.PageSize
            );
        }

        private ActionResult<Pager<PersonaDto>> BadRequest(ApiResponse apiResponse)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Persona>> Post(PersonaDto PersonaDto)
        {
            var Persona = _mapper.Map<Persona>(PersonaDto);
            _unitOfWork.Personas.Add(Persona);
            await _unitOfWork.SaveAsync();
            PersonaDto.Id = Persona.Id;
            return CreatedAtAction(nameof(Post), new { id = PersonaDto.Id }, PersonaDto);
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<PersonaDto>> Put(int id, [FromBody] PersonaDto PersonaDto)
        {
            if (PersonaDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var Persona = _mapper.Map<Persona>(PersonaDto);
            _unitOfWork.Personas.Update(Persona);
            await _unitOfWork.SaveAsync();
            return PersonaDto;
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var Persona = await _unitOfWork.Personas.GetByIdAsync(id);
            _unitOfWork.Personas.Remove(Persona);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
