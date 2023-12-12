using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin,Manager,Employee")]
    public class ContactoPersonaController(IUnitOfWork unitOfWork, IMapper mapper) : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<ContactoPersonaDto>>> Get(
            [FromQuery] Params ContactoPersonaParams
        )
        {
            if (ContactoPersonaParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.ContactoPersonas.GetAllAsync(
                ContactoPersonaParams.PageIndex,
                ContactoPersonaParams.PageSize
            );
            var ContactoPersonaListDto = _mapper.Map<List<ContactoPersonaDto>>(registers);
            return new Pager<ContactoPersonaDto>(
                ContactoPersonaListDto,
                totalRegisters,
                ContactoPersonaParams.PageIndex,
                ContactoPersonaParams.PageSize
            );
        }
        
        [HttpGet("GetContactoDeVigilantes")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<ContactoPersonaDto>>> GetContactoDeVigilantes(
            [FromQuery] Params ContactoPersonaParams
        )
        {
            if (ContactoPersonaParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.ContactoPersonas.GetContactoDeVigilantes(
                ContactoPersonaParams.PageIndex,
                ContactoPersonaParams.PageSize
            );
            var ContactoPersonaListDto = _mapper.Map<List<ContactoPersonaDto>>(registers);
            return new Pager<ContactoPersonaDto>(
                ContactoPersonaListDto,
                totalRegisters,
                ContactoPersonaParams.PageIndex,
                ContactoPersonaParams.PageSize
            );
        }

        private ActionResult<Pager<ContactoPersonaDto>> BadRequest(ApiResponse apiResponse)
        {
        throw new NotImplementedException();
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ContactoPersona>> Post(ContactoPersonaDto ContactoPersonaDto)
        {
            var ContactoPersona = _mapper.Map<ContactoPersona>(ContactoPersonaDto);
            _unitOfWork.ContactoPersonas.Add(ContactoPersona);
            await _unitOfWork.SaveAsync();
            ContactoPersonaDto.Id = ContactoPersona.Id;
            return CreatedAtAction(nameof(Post), new { id = ContactoPersonaDto.Id }, ContactoPersonaDto);
        }
        
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ContactoPersonaDto>> Put(
            int id,
            [FromBody] ContactoPersonaDto ContactoPersonaDto
        )
        {
            if (ContactoPersonaDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var ContactoPersona = _mapper.Map<ContactoPersona>(ContactoPersonaDto);
            _unitOfWork.ContactoPersonas.Update(ContactoPersona);
            await _unitOfWork.SaveAsync();
            return ContactoPersonaDto;
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var ContactoPersona = await _unitOfWork.ContactoPersonas.GetByIdAsync(id);
            _unitOfWork.ContactoPersonas.Remove(ContactoPersona);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }        
    }
}