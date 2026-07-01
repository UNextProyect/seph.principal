using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.HistorialContratos.Commands
{
    public sealed class HistorialContratoCommandValidator
        : AbstractValidator<CreateHistorialContratoCommand>
    {
        public HistorialContratoCommandValidator()
        {
            RuleFor(x => x.DateFechaIngreso)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("La fecha de ingreso no puede ser futura.");

            RuleFor(x => x.DateFechaInicio)
                .NotEmpty()
                .LessThanOrEqualTo(DateTime.Today)
                .WithMessage("La fecha de inicio no puede ser futura.")
                .GreaterThanOrEqualTo(x => x.DateFechaIngreso)
                .WithMessage("La fecha de inicio no puede ser menor a la fecha de ingreso.");

            RuleFor(x => x.IdEmpleado)
                .NotEmpty();

            RuleFor(x => x.IdInstitucion)
                .NotEmpty();

            RuleFor(x => x.IdTipoPersonal)
                .NotEmpty();

            RuleFor(x => x.IdTipoContrato)
                .NotEmpty();

            RuleFor(x => x.StrOtroTipoContrato)
                .MaximumLength(100);

            RuleFor(x => x.IdArea)
                .NotEmpty();

            RuleFor(x => x.DateTimeFechaRegistro)
                .NotEmpty();

            RuleFor(x => x.IdUsuarioRegistro)
                .NotEmpty();

            RuleFor(x => x.DateTimeFechaBaja)
     .GreaterThan(x => x.DateTimeFechaRegistro)
     .When(x => x.DateTimeFechaBaja.HasValue)
     .WithMessage("La fecha de baja no es válida.");
        }
    }
}