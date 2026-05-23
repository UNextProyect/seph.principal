using MediatR;
using Seph.Principal.Application.Common.Models;
using Seph.Principal.Application.Features.Auth.DTOs;

namespace Seph.Principal.Application.Features.Auth.Commands.RefreshToken
{
    public sealed record RefreshTokenCommand(string RefreshToken,string DeviceId,string IpAddress):
        IRequest<ResponseWrapper<AuthResponseDto>>;


}
