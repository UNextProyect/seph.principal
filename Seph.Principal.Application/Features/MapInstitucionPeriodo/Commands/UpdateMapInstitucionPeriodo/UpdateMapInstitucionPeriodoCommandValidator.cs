using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seph.Principal.Application.Features.MapInstitucionPeriodo.Commands.UpdateMapInstitucionPeriodo
{
    /// <summary>
    /// Valida los datos necesarios para actualizar
    /// una asignación de periodo por institución.
    /// </summary>
    public sealed class UpdateMapInstitucionPeriodoCommandValidator
        : AbstractValidator<UpdateMapInstitucionPeriodoCommand>
    {
        public UpdateMapInstitucionPeriodoCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage(
                    "El identificador de la asignación es obligatorio.");

            RuleFor(x => x.IdInstitucion)
                .GreaterThan(0)
                .WithMessage(
                    "La institución es obligatoria.");

            RuleFor(x => x.IdPeriodo)
                .GreaterThan(0)
                .WithMessage(
                    "El periodo es obligatorio.");

            RuleFor(x => x.DateFechaCierre)
                .GreaterThanOrEqualTo(x => x.DateFechaApertura)
                .When(x =>
                    x.DateFechaApertura.HasValue &&
                    x.DateFechaCierre.HasValue)
                .WithMessage(
                    "La fecha de cierre no puede ser menor que la fecha de apertura.");
        }
    }
}
