using MediatR;
using Seph.Principal.Application.Common.Interfaces;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.HistorialContratos.DTOs;
using Seph.Principal.Domain.Entities;
using Seph.Principal.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.HistorialContratos.Commands
{
    public sealed class CreateHistorialContratoCommandHandler(
        IHistorialContratoRepository historialContratoRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<CreateHistorialContratoCommand, ResponseWrapper<HistorialContratoDto>>
    {
        public async Task<ResponseWrapper<HistorialContratoDto>> Handle(
            CreateHistorialContratoCommand request,
            CancellationToken cancellationToken)
        {
            var historialContrato = new HistorialContrato
            {
                DateFechaIngreso = request.DateFechaIngreso,
                DateFechaInicio = request.DateFechaInicio,
                IdEmpleado = request.IdEmpleado,
                IdInstitucion = request.IdInstitucion,
                IdTipoPersonal = request.IdTipoPersonal,
                IdTipoContrato = request.IdTipoContrato,
                StrOtroTipoContrato = request.StrOtroTipoContrato ?? string.Empty,
                IdArea = request.IdArea,
                DateTimeFechaRegistro = request.DateTimeFechaRegistro,
                BitActivo = request.BitActivo,
                DateTimeFechaBaja = request.DateTimeFechaBaja,
                IdUsuarioRegistro = request.IdUsuarioRegistro
            };

            await historialContratoRepository.AddAsync(
                historialContrato,
                cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = new HistorialContratoDto(
                historialContrato.Id,
                historialContrato.DateFechaIngreso,
                historialContrato.DateFechaInicio,
                historialContrato.IdEmpleado,
                historialContrato.IdInstitucion,
                historialContrato.IdTipoPersonal,
                historialContrato.IdTipoContrato,
                historialContrato.StrOtroTipoContrato,
                historialContrato.IdArea,
                historialContrato.DateTimeFechaRegistro,
                historialContrato.BitActivo,
                historialContrato.DateTimeFechaBaja,
                historialContrato.IdUsuarioRegistro);

            return ResponseFactory.Success(
                dto,
                "Historial de contrato registrado correctamente");
        }
    }
}