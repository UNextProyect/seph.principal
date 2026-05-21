namespace Seph.Principal.Domain.Common
{
    public abstract class BaseEntity
    {
        //El id funciona como el identificador general de la entidad, es un Guid para asegurar unicidad a nivel global.
        public Guid Id { get; protected set; } = Guid.NewGuid();
        //CreatedAtUtc y UpdatedAtUtc son propiedades que almacenan la fecha y hora de creación y última actualización de la entidad, respectivamente.
        //Esto es útil para el seguimiento de cambios y auditoría.
        public DateTimeOffset CreatedAtUtc { get; protected set; } = DateTimeOffset.UtcNow;
        
        public DateTimeOffset? UpdatedAtUtc { get; protected set; }
        // El método Touch() se utiliza para actualizar la propiedad UpdatedAtUtc con la fecha
        // y hora actual en formato UTC.
        public void Touch() => UpdatedAtUtc = DateTimeOffset.UtcNow;
    }
}
