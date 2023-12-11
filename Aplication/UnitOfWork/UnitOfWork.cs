using Aplication.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Persistency;
namespace Aplication.UnitOfWork;
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApiDbContext _context;
    private IRolRepository _roles;
    private IUserRepository _users;
    public UnitOfWork(ApiDbContext context)
    {
        _context = context;
    }
    public IRolRepository Roles
    {
        get
        {
            _roles ??= new RolRepository(_context);
            return _roles;
        }
    }

    public IUserRepository Users
    {
        get
        {
            _users ??= new UserRepository(_context);
            return _users;
        }
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    private ICategoriaPersona _CategoriaPersonas;
    public ICategoriaPersona CategoriaPersonas
    {
        get
        {
            _CategoriaPersonas ??= new CategoriaPersonaRepository(_context);
            return _CategoriaPersonas;
        }
    }

    private ICiudad _Ciudades;
    public ICiudad Ciudades
    {
        get
        {
            _Ciudades ??= new CiudadRepository(_context);
            return _Ciudades;
        }
    }

    private IContactoPersona _ContactoPersonas;
    public IContactoPersona ContactoPersonas
    {
        get
        {
            _ContactoPersonas ??= new ContactoPersonaRepository(_context);
            return _ContactoPersonas;
        }
    }

    private IContrato _Contratos;
    public IContrato Contratos
    {
        get
        {
            _Contratos ??= new ContratoRepository(_context);
            return _Contratos;
        }
    }
    private IDepartamento _Departamentos;
    public IDepartamento Departamentos
    {
        get
        {
            _Departamentos ??= new DepartamentoRepository(_context);
            return _Departamentos;
        }
    }
    private IDireccionPersona _DireccionPersonas;
    public IDireccionPersona DireccionPersonas
    {
        get
        {
            _DireccionPersonas ??= new DireccionPersonaRepository(_context);
            return _DireccionPersonas;
        }
    }
    private IEstadoContrato _EstadoContratos;
    public IEstadoContrato EstadoContratos
    {
        get
        {
            _EstadoContratos ??= new EstadoContratoRepository(_context);
            return _EstadoContratos;
        }
    }
    private IPais _Paises;
    public IPais Paises
    {
        get
        {
            _Paises ??= new PaisRepository(_context);
            return _Paises;
        }
    }
    private IPersona _Personas;
    public IPersona Personas
    {
        get
        {
            _Personas ??= new PersonaRepository(_context);
            return _Personas;
        }
    }
    private IProgramacion _Programaciones;
    public IProgramacion Programaciones
    {
        get
        {
            _Programaciones ??= new ProgramacionRepository(_context);
            return _Programaciones;
        }
    }
    private ITipoPersona _TipoPersonas;
    public ITipoPersona TipoPersonas
    {
        get
        {
            _TipoPersonas ??= new TipoPersonaRepository(_context);
            return _TipoPersonas;
        }
    }
    private ITipoContacto _TipoContactos;
    public ITipoContacto TipoContactos
    {
        get
        {
            _TipoContactos ??= new TipoContactoRepository(_context);
            return _TipoContactos;
        }
    }
    private ITipoDireccion _TipoDirecciones;
    public ITipoDireccion TipoDirecciones
    {
        get
        {
            _TipoDirecciones ??= new TipoDireccionRepository(_context);
            return _TipoDirecciones;
        }
    }
    private ITurno _Turnos;
    public ITurno Turnos
    {
        get
        {
            _Turnos ??= new TurnoRepository(_context);
            return _Turnos;
        }
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
