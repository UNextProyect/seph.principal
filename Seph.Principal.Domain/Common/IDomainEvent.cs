using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Common
{
    public interface IDomainEvent
    {
        // La propiedad OccurredAtUtc es de solo lectura y
        // devuelve la fecha y hora en formato UTC en la que ocurrió el evento de dominio.
        DateTimeOffset OccurredAtUtc { get; }
    }
}
