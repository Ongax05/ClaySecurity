using API.Dtos;
using API.Helpers;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CiudadController(IUnitOfWork unitOfWork, IMapper mapper) : ApiBaseController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Pager<CiudadDto>>> Get(
            [FromQuery] Params CiudadParams
        )
        {
            if (CiudadParams == null)
            {
                return BadRequest(new ApiResponse(400, "Params cannot be null"));
            }
            var (totalRegisters, registers) = await _unitOfWork.Ciudades.GetAllAsync(
                CiudadParams.PageIndex,
                CiudadParams.PageSize
            );
            var CiudadListDto = _mapper.Map<List<CiudadDto>>(registers);
            return new Pager<CiudadDto>(
                CiudadListDto,
                totalRegisters,
                CiudadParams.PageIndex,
                CiudadParams.PageSize
            );
        }
        
        private ActionResult<Pager<CiudadDto>> BadRequest(ApiResponse apiResponse)
        {
        throw new NotImplementedException();
        }
        
        [HttpPost]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<Ciudad>> Post(CiudadDto CiudadDto)
        {
            var Ciudad = _mapper.Map<Ciudad>(CiudadDto);
            _unitOfWork.Ciudades.Add(Ciudad);
            await _unitOfWork.SaveAsync();
            CiudadDto.Id = Ciudad.Id;
            return CreatedAtAction(nameof(Post), new { id = CiudadDto.Id }, CiudadDto);
        }
        
        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<CiudadDto>> Put(
            int id,
            [FromBody] CiudadDto CiudadDto
        )
        {
            if (CiudadDto == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var Ciudad = _mapper.Map<Ciudad>(CiudadDto);
            _unitOfWork.Ciudades.Update(Ciudad);
            await _unitOfWork.SaveAsync();
            return CiudadDto;
        }
        
        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Delete(int id)
        {
            var Ciudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
            _unitOfWork.Ciudades.Remove(Ciudad);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}