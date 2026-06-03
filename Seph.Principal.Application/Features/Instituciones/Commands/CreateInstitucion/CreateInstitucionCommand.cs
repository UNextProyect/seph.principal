using MediatR;
using Seph.Principal.Application.Common.Models;

namespace Seph.Principal.Application.Features.Instituciones.Commands.CreateInstitucion
{
    public sealed record CreateInstitucionCommand(string StrValor,string StrDescripcion)
: IRequest<ResponseWrapper<InstitucionDto>>;
}
