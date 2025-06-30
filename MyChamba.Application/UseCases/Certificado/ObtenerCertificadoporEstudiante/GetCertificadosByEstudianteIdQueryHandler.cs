using MediatR;
using MyChamba.Application.Common.Interfaces.Persistence;
using MyChamba.Application.DTOs.Certificados;
using MyChamba.Models;

namespace MyChamba.Application.UseCases.Certificado.ObtenerCertificadoporEstudiante;

public class GetCertificadosByEstudianteIdQueryHandler 
    : IRequestHandler<GetCertificadosByEstudianteIdQuery, List<CertificadoDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCertificadosByEstudianteIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CertificadoDto>> Handle(GetCertificadosByEstudianteIdQuery request, CancellationToken cancellationToken)
    {
        var recompensaRepo = _unitOfWork.Repository<Recompensa>();
        
        var recompensas = await recompensaRepo.FindAsync(r => r.IdEstudiante == request.EstudianteId);

        var certificados = recompensas
            .Where(r => r.RecompensasCertificado != null)
            .Select(r => new CertificadoDto
            {
                IdRecompensa = r.Id,
                Tipo = r.RecompensasCertificado!.Tipo,
                Archivo = r.RecompensasCertificado.Archivo,
                FechaEmision = r.RecompensasCertificado.FechaEmision
            })
            .ToList();

        return certificados;
    }
}