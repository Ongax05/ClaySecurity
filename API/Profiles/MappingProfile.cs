using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Domain.Entities;

namespace API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile (){
            CreateMap<CategoriaPersona,CategoriaPersonaDto>().ReverseMap();
            CreateMap<Ciudad,CiudadDto>().ReverseMap();
            CreateMap<ContactoPersona,ContactoPersonaDto>().ReverseMap();
            CreateMap<Contrato,ContratoDto>().ReverseMap();
            CreateMap<Departamento,DepartamentoDto>().ReverseMap();
            CreateMap<DireccionPersona,DireccionPersonaDto>().ReverseMap();
            CreateMap<EstadoContrato,EstadoContratoDto>().ReverseMap();
            CreateMap<Pais,PaisDto>().ReverseMap();
            CreateMap<Persona,PersonaDto>().ReverseMap();
            CreateMap<Programacion,ProgramacionDto>().ReverseMap();
            CreateMap<TipoContacto,TipoDto>().ReverseMap();
            CreateMap<TipoDireccion,TipoDto>().ReverseMap();
            CreateMap<TipoPersona,TipoDto>().ReverseMap();
            CreateMap<Turno,TurnoDto>().ReverseMap();
        }
    }
}