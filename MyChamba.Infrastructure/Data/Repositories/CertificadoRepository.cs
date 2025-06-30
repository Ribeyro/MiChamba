using Microsoft.EntityFrameworkCore;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.DTOs.Certificados;
using MyChamba.Data;

namespace MyChamba.Infrastructure.Data.Repositories;

public class CertificadoRepository : ICertificadoRepository
{
    private readonly MyChambaContext _context;

    public CertificadoRepository(MyChambaContext context)
    {
        _context = context;
    }

    public async Task<List<CertificadoDto>> GetCertificadosByEstudianteIdAsync(ulong estudianteId)
    {
        return await _context.Recompensas
            .Where(r => r.IdEstudiante == estudianteId && r.RecompensasCertificado != null)
            .Select(r => new CertificadoDto
            {
                IdRecompensa = r.Id,
                Tipo = r.RecompensasCertificado!.Tipo,
                Archivo = r.RecompensasCertificado.Archivo,
                FechaEmision = r.RecompensasCertificado.FechaEmision
            })
            .ToListAsync();
    }
}