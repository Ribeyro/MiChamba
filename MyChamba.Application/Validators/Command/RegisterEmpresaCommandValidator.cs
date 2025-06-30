using FluentValidation;
using MyChamba.Application.UseCases.Usuarios.Commands;

namespace MyChamba.Application.Validators;

public class RegisterEmpresaCommandValidator : AbstractValidator<RegisterEmpresaCommand>
{
    public RegisterEmpresaCommandValidator()
    {
        // Validar Email
        RuleFor(x => x.Request.Email)
            .NotEmpty().WithMessage("El Email es obligatorio.")
            .EmailAddress().WithMessage("El Email no es válido.");

        // Validar Password
        RuleFor(x => x.Request.Password)
            .NotEmpty().WithMessage("La contraseña es obligatoria.")
            .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

        // Validar datos de la Empresa
        RuleFor(x => x.Request.Empresa.Nombre)
            .NotEmpty().WithMessage("El nombre de la empresa es obligatorio.");

        RuleFor(x => x.Request.Empresa.Telefono)
            .NotEmpty().WithMessage("El teléfono es obligatorio.");

        RuleFor(x => x.Request.Empresa.Direccion)
            .NotEmpty().WithMessage("La dirección es obligatoria.");

        RuleFor(x => x.Request.Empresa.Logo)
            .NotEmpty().WithMessage("El logo es obligatorio.");

        RuleFor(x => x.Request.Empresa.IdSector)
            .GreaterThan(0).WithMessage("El IdSector debe ser mayor a 0.");

        // ✅ Validación estructural del RUC (sin verificador matemático)
        RuleFor(x => x.Request.Empresa.Ruc)
            .NotEmpty().WithMessage("El RUC es obligatorio.")
            .Length(11).WithMessage("El RUC debe tener exactamente 11 dígitos.")
            .Must(ruc => ruc.StartsWith("20")).WithMessage("El RUC debe empezar con '20'.")
            .Must(ruc => ruc.All(char.IsDigit)).WithMessage("El RUC debe contener solo números.");
    }
}