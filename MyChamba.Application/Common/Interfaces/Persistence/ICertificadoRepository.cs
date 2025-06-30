using MyChamba.Application.DTOs.Certificados;

namespace MyChamba.Application.Common.Interfaces.Persistence;

public interface ICertificadoRepository
{
    Task<List<CertificadoDto>> GetCertificadosByEstudianteIdAsync(ulong estudianteId);
}