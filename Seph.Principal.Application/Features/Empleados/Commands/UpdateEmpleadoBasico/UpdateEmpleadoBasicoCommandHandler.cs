using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Domain.Repositories;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Empleados.Commands.UpdateEmpleadoBasico
{
    public sealed class UpdateEmpleadoBasicoCommandHandler(
        IEmpleadosRepository empleadosRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateEmpleadoBasicoCommand, ResponseWrapper<string>>
    {
        public async Task<ResponseWrapper<string>> Handle(
            UpdateEmpleadoBasicoCommand request,
            CancellationToken cancellationToken)
        {
            var empleado = await empleadosRepository.GetByIdAsync(request.IdEmpleado, cancellationToken);

            if (empleado is null)
            {
                return ResponseFactory.Failure<string>("Empleado no encontrado", HttpStatusCode.NotFound);
            }

            empleado.StrNombre = request.StrNombre;
            empleado.StrApellidoPat = request.StrApellidoPat;
            empleado.StrApellidoMat = request.StrApellidoMat;
            empleado.StrCurp = request.StrCurp;
            empleado.IdSexo = request.IdSexo;

            if (!string.IsNullOrWhiteSpace(request.StrRutaIne))
            {
                empleado.StrRutaIne = request.StrRutaIne;
            }

            if (!string.IsNullOrWhiteSpace(request.StrRutaFotografia))
            {
                empleado.StrRutaFotografia = request.StrRutaFotografia;
            }

            empleadosRepository.Update(empleado);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ResponseFactory.Success(
                "Información personal actualizada correctamente",
                "Información personal actualizada correctamente");
        }
    }
}
