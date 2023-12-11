using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProgramacionController : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public ProgramacionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<ProgramacionDto>>> Get(
            [FromQuery] Params ProgramacionParams
        )
        {
            if (ProgramacionParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.Programaciones.GetAllAsync(
                ProgramacionParams.PageIndex,
                ProgramacionParams.PageSize
            );
            var ProgramacionListDto = _mapper.Map<List<ProgramacionDto>>(registers);
            return new Pager<ProgramacionDto>(
                ProgramacionListDto,
                totalRegisters,
                ProgramacionParams.PageIndex,
                ProgramacionParams.PageSize
            );
        }
        
        private ActionResult<Pager<ProgramacionDto>> BadRequest(ApiResponse apiResponse)
        {
        throw new NotImplementedException();
        }
        
        [HttpGet]
        [MapToApiVersion("1.1")]
        public async Task<ActionResult<IEnumerable<ProgramacionDto>>> Get1_1()
        {
            var registers = await _unitOfWork.Programaciones.GetAllAsync();
            var ProgramacionListDto = _mapper.Map<List<ProgramacionDto>>(registers);
            return ProgramacionListDto;
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Programacion>> Post(ProgramacionDto ProgramacionDto)
        {
            var Programacion = _mapper.Map<Programacion>(ProgramacionDto);
            _unitOfWork.Programaciones.Add(Programacion);
            await _unitOfWork.SaveAsync();
            ProgramacionDto.Id = Programacion.Id;
            return CreatedAtAction(nameof(Post), new { id = ProgramacionDto.Id }, ProgramacionDto);
        }
        
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ProgramacionDto>> Put(
            int id,
            [FromBody] ProgramacionDto ProgramacionDto
        )
        {
            if (ProgramacionDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var Programacion = _mapper.Map<Programacion>(ProgramacionDto);
            _unitOfWork.Programaciones.Update(Programacion);
            await _unitOfWork.SaveAsync();
            return ProgramacionDto;
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var Programacion = await _unitOfWork.Programaciones.GetByIdAsync(id);
            _unitOfWork.Programaciones.Remove(Programacion);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}