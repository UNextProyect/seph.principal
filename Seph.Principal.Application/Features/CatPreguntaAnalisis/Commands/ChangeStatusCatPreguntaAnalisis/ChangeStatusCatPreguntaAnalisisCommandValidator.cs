using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.ChangeStatusCatPreguntaAnalisis
{
    /// <summary>
    /// Valida la solicitud para cambiar
    /// el estado de una pregunta de análisis.
    /// </summary>
    public sealed class ChangeStatusCatPreguntaAnalisisCommandValidator
        : AbstractValidator<ChangeStatusCatPreguntaAnalisisCommand>
    {
        public ChangeStatusCatPreguntaAnalisisCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage(
                    "El identificador de la pregunta es obligatorio.");
        }
    }
}
