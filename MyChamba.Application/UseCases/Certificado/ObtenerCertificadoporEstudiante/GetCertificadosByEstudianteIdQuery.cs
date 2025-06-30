using MediatR;
using MyChamba.Application.DTOs.Certificados;

namespace MyChamba.Application.UseCases.Certificado.ObtenerCertificadoporEstudiante;

public record GetCertificadosByEstudianteIdQuery(ulong EstudianteId) 
    : IRequest<List<CertificadoDto>>;