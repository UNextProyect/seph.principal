using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seph.Principal.Application.Features.CatPreguntaAnalisis.Commands.CreateCatPreguntaAnalisis
{
    /// <summary>
    /// Validador para el registro
    /// de una pregunta de análisis estratégico.
    /// </summary>
    public sealed class CreateCatPreguntaAnalisisCommandValidator
        : AbstractValidator<CreateCatPreguntaAnalisisCommand>
    {
        public CreateCatPreguntaAnalisisCommandValidator()
        {
            RuleFor(x => x.StrPregunta)
                .NotEmpty()
                .WithMessage(
                    "El texto de la pregunta es obligatorio.")
                .Must(strPregunta =>
                    !string.IsNullOrWhiteSpace(strPregunta))
                .WithMessage(
                    "El texto de la pregunta es obligatorio.")
                .MaximumLength(300)
                .WithMessage(
                    "El texto de la pregunta no puede exceder los 300 caracteres.");
        }
    }

}
