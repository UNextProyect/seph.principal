using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Empleados.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.Empleados.Commands.CreateEmpleado
{
    public sealed class CreateEmpleadoCommandHandler(IEmpleadosRepository empleadosRepository,
     IUnitOfWork unitOfWork): IRequestHandler<CreateEmpleadoCommand, ResponseWrapper<EmpleadoResponse>>
    {
        public async Task<ResponseWrapper<EmpleadoResponse>> Handle(CreateEmpleadoCommand request, CancellationToken cancellationToken)
        {
            var empleado = new Empleado
            {
                StrNombre = request.StrNombre,
                StrApellidoPat = request.StrApellidoPat,
                StrApellidoMat = request.StrApellidoMat,
                StrCurp = request.StrCurp,
                IdSexo = request.IdSexo,
                IdInstitucion = request.IdInstitucion,
                DateTimeFechaRegistro = request.DateTimeFechaRegistro,
                IdUsuarioRegistro = request.IdUsuarioRegistro,
                BitActivo = request.BitActivo,
                DateTimeFechaBaja = request.DateTimeFechaBaja,
            };



            await empleadosRepository.AddAsync(empleado, cancellationToken);
            
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return ResponseFactory.Success(
             new EmpleadoResponse(
                 empleado.Id,
                 empleado.StrNombre,
                 empleado.StrApellidoPat,
                 empleado.StrApellidoMat,
                 empleado.StrCurp),
             "Empleado registrado correctamente");
        }
    }
}
