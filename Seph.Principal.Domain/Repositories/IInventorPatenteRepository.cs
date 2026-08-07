using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    /// <summary>
    /// Define las operaciones de acceso a datos
    /// para los inventores de una patente.
    /// </summary>
    public interface IInventorPatenteRepository
    {
        /// <summary>
        /// Obtiene todos los inventores asociados
        /// a un reporte de patente.
        /// </summary>
        Task<IReadOnlyList<InventorPatente>> GetByIdPatenteAsync(
            long idPatente,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega un nuevo inventor
        /// a la patente correspondiente.
        /// </summary>
        Task AddAsync(
            InventorPatente inventorPatente,
            CancellationToken cancellationToken);

        /// <summary>
        /// Agrega una colección de inventores
        /// a la patente correspondiente.
        /// </summary>
        Task AddRangeAsync(
            IEnumerable<InventorPatente> inventoresPatente,
            CancellationToken cancellationToken);

        /// <summary>
        /// Elimina todos los inventores asociados
        /// a un reporte de patente.
        /// </summary>
        Task DeleteByIdPatenteAsync(
            long idPatente,
            CancellationToken cancellationToken);

        /// <summary>
        /// Marca un inventor
        /// para eliminación.
        /// </summary>
        void Delete(
            InventorPatente inventorPatente);
    }
}