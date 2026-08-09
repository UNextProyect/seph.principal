using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Empleados.DTOs;
using Seph.Principal.Domain.Repositories;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Empleados.Queries.GetEmpleadoById
{
    public sealed class GetEmpleadoByIdQueryHandler(
        IEmpleadosRepository empleadosRepository,
        IMapEmpleadoPerfilAcademicoRepository mapEmpleadoPerfilAcademicoRepository)
        : IRequestHandler<GetEmpleadoByIdQuery, ResponseWrapper<EmpleadoDetailDto>>
    {
        public async Task<ResponseWrapper<EmpleadoDetailDto>> Handle(
            GetEmpleadoByIdQuery request,
            CancellationToken cancellationToken)
        {
            var empleado = await empleadosRepository.GetByIdAsync(request.IdEmpleado, cancellationToken);

            if (empleado is null)
            {
                return ResponseFactory.Failure<EmpleadoDetailDto>("Empleado no encontrado", HttpStatusCode.NotFound);
            }

            var perfiles = await mapEmpleadoPerfilAcademicoRepository.GetByEmpleadoIdAsync(request.IdEmpleado, cancellationToken);

            var dto = new EmpleadoDetailDto(
                empleado.Id,
                empleado.StrNombre ?? string.Empty,
                empleado.StrApellidoPat ?? string.Empty,
                empleado.StrApellidoMat ?? string.Empty,
                empleado.StrCurp ?? string.Empty,
                empleado.IdSexo,
                empleado.StrSNII,
                empleado.StrRutaIne,
                empleado.StrRutaFotografia,
                perfiles.Select(p => p.IdCatPerfilAcademico).ToList(),
                empleado.BitDatosAcademicosCompletos);

            return ResponseFactory.Success(dto, "Empleado obtenido correctamente");
        }
    }
}
