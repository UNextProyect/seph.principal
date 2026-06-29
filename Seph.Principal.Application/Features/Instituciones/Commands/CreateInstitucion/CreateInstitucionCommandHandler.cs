using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.Instituciones.Commands.CreateInstitucion
{
    public sealed class CreateInstitucionCommandHandler(
        IInstitucionRepository institucionRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<CreateInstitucionCommand, ResponseWrapper<InstitucionDto>>
    {
        public async Task<ResponseWrapper<InstitucionDto>> Handle(CreateInstitucionCommand request, CancellationToken cancellationToken)
        {
            var institucion = new Institucion
            {
                StrNombre = request.StrNombre,
                StrSiglas = request.StrSiglas,
                StrCct = request.StrCct,
                StrDireccion = request.StrDireccion,
                DateFechaCreacion = request.DateFechaCreacion,
                StrDecretoCreacion = request.StrDecretoCreacion,
                StrSitioWeb = request.StrSitioWeb,
                StrCorreoInstitucional = request.StrCorreoInstitucional,
                StrTelefonoInstitucional = request.StrTelefonoInstitucional,
                IdMunicipio = request.IdMunicipio,
                DateTimeFechaRegistro = DateTime.UtcNow,
                BitActivo = true
            };

            await institucionRepository.AddAsync(institucion, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = new InstitucionDto(
                institucion.Id,
                institucion.StrNombre,
                institucion.StrSiglas,
                institucion.StrCct,
                institucion.StrDireccion,
                institucion.StrSitioWeb,
                institucion.StrCorreoInstitucional,
                institucion.StrTelefonoInstitucional,
                institucion.IdMunicipio,
                institucion.BitActivo);

            return ResponseFactory.Success(dto, "Institución registrada correctamente");
        }
    }
}
