using System.Net;
using System.Net.Mail;
using MyChamba.Data.UnitofWork;
using MyChamba.Models;
using MyChamba.Services.Interfaces;

namespace MyChamba.Services.Implementations;

public class VerificacionService: IVerificacionService
{
    private readonly IUnitOfWork _unitOfWork;

    public VerificacionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task EnviarCorreoVerificacionAsync(Usuario usuario)
    {
        var token = Guid.NewGuid().ToString(); // generar token único
        usuario.TokenVerificacion = token;

        _unitOfWork.Repository<Usuario>().Update(usuario);
        await _unitOfWork.Complete();

        var linkVerificacion = $"https://tuservidor.com/api/usuario/verificar?token={token}";

        // Configuración básica de SMTP (puedes reemplazar luego con SendGrid)
        using var client = new SmtpClient("smtp.gmail.com", 587)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential("ribeyro.quispe@tecsup.edu.pe", "jnbzliwloytijapn") // sin espacios
        };

        var mensaje = new MailMessage("tuemail@gmail.com", usuario.Email)
        {
            Subject = "Verifica tu correo - MyChamba",
            Body = $"Haz clic en el siguiente enlace para verificar tu correo:\n{linkVerificacion}"
        };

        await client.SendMailAsync(mensaje);
    }

    public async Task<bool> VerificarCorreoAsync(string token)
    {
        var usuario = (await _unitOfWork.Repository<Usuario>()
                .FindAsync(u => u.TokenVerificacion == token))
            .FirstOrDefault();

        if (usuario == null) return false;

        usuario.EmailVerificado = true;
        usuario.TokenVerificacion = null;

        _unitOfWork.Repository<Usuario>().Update(usuario);
        await _unitOfWork.Complete();

        return true;
    }
}