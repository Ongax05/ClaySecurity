namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IRolRepository Roles { get; }
    IUserRepository Users { get; }
    Task<int> SaveAsync();
    ICategoriaPersona CategoriaPersonas { get; }
    ICiudad Ciudades { get; }
    IContactoPersona ContactoPersonas { get; }
    IContrato Contratos { get; }
    IDepartamento Departamentos { get; }
    IDireccionPersona DireccionPersonas { get; }
    IEstadoContrato EstadoContratos { get; }
    IPais Paises { get; }
    IPersona Personas { get; }
    IProgramacion Programaciones { get; }
    ITipoPersona TipoPersonas { get; }
    ITipoContacto TipoContactos { get; }
    ITipoDireccion TipoDirecciones { get; }
    ITurno Turnos { get; }
}
