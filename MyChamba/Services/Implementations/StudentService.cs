using MyChamba.Data.Repositories.Student;
using MyChamba.DTOs.Proyecto;

namespace MyChamba.Services.Implementations.Student;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<List<ProyectoCompletoDto>> ObtenerProyectosDisponiblesAsync()
    {
        return await _studentRepository.ObtenerProyectosCompletosAsync();
    }

    public async Task<List<ProyectoCompletoDto>> ObtenerProyectosFiltradosAsync(string fechaTexto, uint? idEmpresa = null)
    {
        return await _studentRepository.ObtenerProyectosFiltradosAsync(fechaTexto, idEmpresa);
    }
}