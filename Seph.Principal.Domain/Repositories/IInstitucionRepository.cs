using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Domain.Repositories
{
    public interface IInstitucionRepository
    {
        Task<IReadOnlyList<Institucion>> GetAllAsync(CancellationToken cancellationToken);

        Task<Institucion?> GetByIdAsync(long id,CancellationToken cancellationToken);

        Task AddAsync(Institucion institucion,CancellationToken cancellationToken);

        void Update(Institucion institucion);

        void Delete(Institucion institucion);
    }
}
