using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seph.Principal.Domain.Common
{
    public abstract class AuditableEntity: BaseEntity
    {
        //En esta clase se agregan dos propiedades adicionales, CreatedBy y UpdatedBy,
        //que almacenan información sobre quién creó o actualizó la entidad.
        public string? CreatedBy { get; protected set; }
        public string? UpdatedBy { get; protected set; }
        // Los métodos MarkCreated y MarkUpdated se utilizan para establecer estas propiedades
        // cuando se crea o actualiza
        // la entidad.
        public void MarkCreated(string? actor) => CreatedBy = actor;

        public void MarkUpdated(string? actor)
        {
            UpdatedBy = actor;
            Touch();
        }
    }
}
