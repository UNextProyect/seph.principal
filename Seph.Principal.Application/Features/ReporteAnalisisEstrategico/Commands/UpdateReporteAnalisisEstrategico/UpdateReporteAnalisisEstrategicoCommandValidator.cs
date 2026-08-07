using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Seph.Principal.Application.Features.ReporteAnalisisEstrategico.Commands.UpdateReporteAnalisisEstrategico
{
    /// <summary>
    /// Valida los datos necesarios para actualizar
    /// un reporte de análisis estratégico.
    /// </summary>
    public sealed class UpdateReporteAnalisisEstrategicoCommandValidator
        : AbstractValidator<UpdateReporteAnalisisEstrategicoCommand>
    {
        public UpdateReporteAnalisisEstrategicoCommandValidator()
        {
            RuleFor(x => x.IdMapInstitucionPeriodo)
                .GreaterThan(0)
                .WithMessage(
                    "El identificador del periodo institucional es obligatorio.");

            /*
             * La colección debe existir, pero puede permanecer
             * vacía porque las respuestas no son obligatorias.
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
                     * Verifica que cada respuesta tenga
                     * un identificador de pregunta válido.
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
                     * Evita recibir dos respuestas
                     * correspondientes a la misma pregunta.
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
