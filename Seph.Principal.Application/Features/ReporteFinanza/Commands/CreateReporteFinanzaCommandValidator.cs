using FluentValidation;

namespace Seph.Principal.Application.Features.ReporteFinanza.Commands
{
    /// <summary>
    /// Validador para el registro
    /// de un reporte financiero.
    /// </summary>
    public sealed class CreateReporteFinanzaCommandValidator
        : AbstractValidator<CreateReporteFinanzaCommand>
    {
        public CreateReporteFinanzaCommandValidator()
        {
            RuleFor(x => x.IdMapInstitucionPeriodo)
                .GreaterThan(0)
                .WithMessage("El identificador del periodo es obligatorio.");

            RuleFor(x => x.MoneyPresupuestoAnual)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El presupuesto anual no puede ser negativo.");

            RuleFor(x => x.MoneySubsidioEstatal)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El subsidio estatal no puede ser negativo.");

            RuleFor(x => x.MoneySubsidioFederal)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El subsidio federal no puede ser negativo.");

            RuleFor(x => x.MoneyIngresosPropios)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Los ingresos propios no pueden ser negativos.");

            RuleFor(x => x.MoneyGastoEjercido)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El gasto ejercido no puede ser negativo.");

            RuleFor(x => x.MoneyGastoAlumno)
                .GreaterThanOrEqualTo(0)
                .WithMessage("El gasto por alumno no puede ser negativo.");

            When(x => x.BitAdeudos, () =>
            {
                RuleFor(x => x.MoneyMontoAdeudo)
                    .GreaterThan(0)
                    .WithMessage(
                        "El monto del adeudo debe ser mayor que cero.");
            });

            RuleFor(x => x.ProyectosFinanciados)
                .NotNull()
                .Must(x => x.Any())
                .WithMessage(
                    "Debe registrar al menos un proyecto financiado.");
        }
    }
}