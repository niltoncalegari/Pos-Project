using Pos_Repository.Repositories;

namespace Pos_Repository;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Gets a non-generic repository from DI container
    /// </summary>
    /// <typeparam name="TCustomRepository"></typeparam>
    /// <returns></returns>
    TCustomRepository? GetCustomRepository<TCustomRepository>() where TCustomRepository : class;

    /// <summary>
    /// Gets a generic repository from DI container
    /// </summary>
    /// <typeparam name="TCustomRepository"></typeparam>
    /// <returns></returns>
    IRepository<TEntity>? GetRepositoryFor<TEntity>() where TEntity : class;

    /// <summary>
    /// Saves all pending changes in the current context
    /// </summary>
    /// <returns></returns>
    Task<int> SaveChangesAsync();
}
