using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seph.Principal.Application.Features.ReporteAnalisisEstrategico.Commands.CreateReporteAnalisisEstrategico
{
    /// <summary>
    /// Valida los datos necesarios para registrar
    /// un reporte de análisis estratégico.
    /// </summary>
    public sealed class CreateReporteAnalisisEstrategicoCommandValidator
        : AbstractValidator<CreateReporteAnalisisEstrategicoCommand>
    {
        public CreateReporteAnalisisEstrategicoCommandValidator()
        {
            RuleFor(x => x.IdMapInstitucionPeriodo)
                .GreaterThan(0)
                .WithMessage(
                    "El identificador del periodo institucional es obligatorio.");

            RuleFor(x => x.IdUsuarioRegistro)
                .NotEmpty()
                .WithMessage(
                    "El identificador del usuario es obligatorio.");

            /*
             * La colección debe existir, pero puede estar vacía
             * porque las respuestas no son obligatorias.
             */
            RuleFor(x => x.RespuestasAnalisis)
                .NotNull()
                .WithMessage(
                    "La colección de respuestas es obligatoria.");

            When(
                x => x.RespuestasAnalisis is not null,
                () =>
                {
                    /*
                     * Valida que cada respuesta corresponda
                     * a una pregunta válida.
                     */
                    RuleForEach(x => x.RespuestasAnalisis)
                        .ChildRules(
                            respuesta =>
                            {
                                respuesta
                                    .RuleFor(
                                        x => x.IdPreguntaAnalisis)
                                    .GreaterThan(0)
                                    .WithMessage(
                                        "El identificador de la pregunta es obligatorio.");
                            });

                    /*
                     * Evita enviar más de una respuesta
                     * para la misma pregunta.
                     */
                    RuleFor(x => x.RespuestasAnalisis)
                        .Must(
                            respuestas =>
                                respuestas
                                    .Select(
                                        x => x.IdPreguntaAnalisis)
                                    .Distinct()
                                    .Count()
                                == respuestas.Count)
                        .WithMessage(
                            "No se puede registrar más de una respuesta para la misma pregunta.");
                });
        }
    }
}
