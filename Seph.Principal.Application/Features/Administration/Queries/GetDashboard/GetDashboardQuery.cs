using MediatR;
using Seph.Principal.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Application.Features.Administration.Queries.GetDashboard
{
    public sealed record GetDashboardQuery: IRequest<ResponseWrapper<DashboardSummaryDto>>;
    public sealed record DashboardSummaryDto(
    int ActiveUsers,
    int ActiveSessions,
    int SecurityAlerts,
    decimal ApiAvailability,
    IReadOnlyCollection<DashboardActivityDto> RecentActivities);

    public sealed record DashboardActivityDto(string Title, string Actor, DateTimeOffset OccurredAtUtc, string Severity);


}
