using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Commands.CreateMapInstitucionPeriodo
{
    public sealed class CreateMapInstitucionPeriodoCommandValidator
         : AbstractValidator<CreateMapInstitucionPeriodoCommand>
    {
        public CreateMapInstitucionPeriodoCommandValidator()
        {
            RuleFor(x => x.IdInstitucion)
                .NotEmpty();

            RuleFor(x => x.IdPeriodo)
                .NotEmpty();

            RuleFor(x => x.IdUsuarioRegistro)
                .NotEmpty();

            RuleFor(x => x.DateFechaCierre)
                .GreaterThanOrEqualTo(x => x.DateFechaApertura)
                .When(x => x.DateFechaApertura.HasValue && x.DateFechaCierre.HasValue)
                .WithMessage("La fecha de cierre no puede ser menor a la fecha de apertura.");
        }
    }
}
