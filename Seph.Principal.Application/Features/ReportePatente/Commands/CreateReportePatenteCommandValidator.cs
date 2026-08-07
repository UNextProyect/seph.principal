using FluentValidation;
using System.Linq;

namespace Seph.Principal.Application.Features.ReportePatente.Commands.CreateReportePatente
{
    /// <summary>
    /// Validador para el registro
    /// de un reporte de patente.
    /// </summary>
    public sealed class CreateReportePatenteCommandValidator
        : AbstractValidator<CreateReportePatenteCommand>
    {
        public CreateReportePatenteCommandValidator()
        {
            RuleFor(x => x.IdMapInstitucionPeriodo)
                .GreaterThan(0)
                .WithMessage(
                    "El identificador del periodo es obligatorio.");

            RuleFor(x => x.StrNombreTitulo)
                .NotEmpty()
                .WithMessage(
                    "El nombre o título de la patente es obligatorio.")
                .MaximumLength(200)
                .WithMessage(
                    "El nombre o título de la patente no puede exceder los 200 caracteres.");

            RuleFor(x => x.StrNumeroRegistroSolicitud)
                .NotEmpty()
                .WithMessage(
                    "El número de registro o solicitud es obligatorio.")
                .MaximumLength(100)
                .WithMessage(
                    "El número de registro o solicitud no puede exceder los 100 caracteres.");

            RuleFor(x => x.IdTipoPatente)
                .GreaterThan(0)
                .WithMessage(
                    "Debe seleccionar un tipo de patente.");

            RuleFor(x => x.IdEstatusPatente)
                .GreaterThan(0)
                .WithMessage(
                    "Debe seleccionar un estatus de patente.");

            RuleFor(x => x.DateFechaSolicitud)
                .NotEmpty()
                .WithMessage(
                    "La fecha de solicitud es obligatoria.");

            When(x => x.DateFechaConcesion.HasValue, () =>
            {
                RuleFor(x => x.DateFechaConcesion)
                    .GreaterThanOrEqualTo(x => x.DateFechaSolicitud)
                    .WithMessage(
                        "La fecha de concesión no puede ser anterior a la fecha de solicitud.");
            });

            RuleFor(x => x.StrTitularPatente)
                .NotEmpty()
                .WithMessage(
                    "El titular de la patente es obligatorio.")
                .MaximumLength(200)
                .WithMessage(
                    "El titular de la patente no puede exceder los 200 caracteres.");

            RuleFor(x => x.Inventores)
                .NotNull()
                .Must(x => x.Any())
                .WithMessage(
                    "Debe registrar al menos un inventor.");

            RuleForEach(x => x.Inventores)
                .ChildRules(inventor =>
                {
                    inventor.RuleFor(x => x.StrNombreCompleto)
                        .NotEmpty()
                        .WithMessage(
                            "El nombre completo del inventor es obligatorio.")
                        .MaximumLength(200)
                        .WithMessage(
                            "El nombre completo del inventor no puede exceder los 200 caracteres.");
                });
        }
    }
}