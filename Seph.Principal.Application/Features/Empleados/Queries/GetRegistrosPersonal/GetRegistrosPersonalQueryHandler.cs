using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Empleados.DTOs;
using Seph.Principal.Domain.Repositories;

namespace Seph.Principal.Application.Features.Empleados.Queries.GetRegistrosPersonal
{
    /// <summary>
    /// Arma el concentrado: empleados capturados por el usuario + su historial de contrato,
    /// resolviendo los textos de los catálogos (sexo, institución, tipo personal, tipo contrato, área).
    /// </summary>
    public sealed class GetRegistrosPersonalQueryHandler(
        IEmpleadosRepository empleadosRepository,
        IHistorialContratoRepository historialContratoRepository,
        ICatSexoRepository catSexoRepository,
        ICatTipoPersonalRepository catTipoPersonalRepository,
        ICatTipoContratoRepository catTipoContratoRepository,
        ICatAreaRepository catAreaRepository,
        IInstitucionRepository institucionRepository,
        ICatPerfilAcademicoRepository catPerfilAcademicoRepository,
        IMapEmpleadoPerfilAcademicoRepository mapEmpleadoPerfilAcademicoRepository)
        : IRequestHandler<GetRegistrosPersonalQuery, ResponseWrapper<IReadOnlyList<RegistroPersonalDto>>>
    {
        public async Task<ResponseWrapper<IReadOnlyList<RegistroPersonalDto>>> Handle(
            GetRegistrosPersonalQuery request, CancellationToken cancellationToken)
        {
            var empleados = await empleadosRepository.GetAllAsync(cancellationToken);
            var historiales = await historialContratoRepository.GetAllAsync(cancellationToken);
            var sexos = await catSexoRepository.GetAllAsync(cancellationToken);
            var tiposPersonal = await catTipoPersonalRepository.GetAllAsync(cancellationToken);
            var tiposContrato = await catTipoContratoRepository.GetAllAsync(cancellationToken);
            var areas = await catAreaRepository.GetAllAsync(cancellationToken);
            var instituciones = await institucionRepository.GetAllAsync(cancellationToken);
            var perfilesAcademicos = await catPerfilAcademicoRepository.GetAllAsync(cancellationToken);

            var empleadosDelUsuario = empleados
                .Where(empleado => empleado.IdUsuarioRegistro == request.IdUsuarioRegistro)
                .ToList();

            var lista = new List<RegistroPersonalDto>();

            foreach (var empleado in empleadosDelUsuario.OrderByDescending(e => e.DateTimeFechaRegistro))
            {
                // Contrato más reciente del empleado (null si el registro quedó incompleto).
                var contrato = historiales
                    .Where(historial => historial.IdEmpleado == empleado.Id)
                    .OrderByDescending(historial => historial.DateTimeFechaRegistro)
                    .FirstOrDefault();

                var mapaPerfiles = await mapEmpleadoPerfilAcademicoRepository.GetByEmpleadoIdAsync(empleado.Id, cancellationToken);
                var nombresPerfiles = mapaPerfiles
                    .Select(m => perfilesAcademicos.FirstOrDefault(p => p.Id == m.IdCatPerfilAcademico)?.StrValor)
                    .Where(nombre => nombre is not null)
                    .Select(nombre => nombre!)
                    .ToList();

                lista.Add(new RegistroPersonalDto(
                    empleado.Id,
                    empleado.StrNombre ?? string.Empty,
                    empleado.StrApellidoPat ?? string.Empty,
                    empleado.StrApellidoMat ?? string.Empty,
                    empleado.StrCurp ?? string.Empty,
                    sexos.FirstOrDefault(sexo => sexo.Id == empleado.IdSexo)?.StrValor ?? string.Empty,
                    empleado.DateTimeFechaRegistro,
                    empleado.BitActivo,
                    empleado.BitDatosAcademicosCompletos,
                    contrato is not null,
                    empleado.StrSNII,
                    empleado.StrRutaIne,
                    empleado.StrRutaFotografia,
                    nombresPerfiles,
                    contrato is null ? null : instituciones.FirstOrDefault(i => i.Id == contrato.IdInstitucion)?.StrNombre,
                    contrato is null ? null : tiposPersonal.FirstOrDefault(t => t.Id == contrato.IdTipoPersonal)?.StrValor,
                    contrato is null ? null : tiposContrato.FirstOrDefault(t => t.Id == contrato.IdTipoContrato)?.StrValor,
                    contrato is null ? null : areas.FirstOrDefault(a => a.Id == contrato.IdArea)?.StrValor,
                    contrato?.DateFechaIngreso));
            }

            return ResponseFactory.Success(
                (IReadOnlyList<RegistroPersonalDto>)lista,
                "Registros de personal obtenidos correctamente");
        }
    }
}
