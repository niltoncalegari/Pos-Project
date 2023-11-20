using System.Linq.Expressions;

namespace Pos_Repository.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
    void Attach<T>(T entity)
        where T : class;
    void Detach<T>(T entity)
        where T : class;
    void SetIsModified<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty?>> propertyExpression)
        where TProperty : class;

    /// <summary>
    /// Allows search using LINQ for filtering and sorting and can also load other entities
    /// base on the relationship with the main entity
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties">Comma separated entity list to include (join)</param>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", int? take = null, int? skip = null);

    /// <summary>
    /// Allows search using LINQ for filtering and sorting and can also load other entities
    /// base on the relationship with the main entity Without Tracking
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties">Comma separated entity list to include (join)</param>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        int? take = null,
        int? skip = null);

    Task<IEnumerable<TEntity>> GetAsTrackingAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        int? take = null,
        int? skip = null);

    /// <summary>
    /// Allows search specific properties using LINQ for filtering and sorting and can also load other entities
    /// base on the relationship with the main entity
    /// </summary>
    /// <param name="select"></param>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties"></param>
    /// <param name="take"></param>
    /// <param name="skip"></param>
    /// <returns></returns>
    Task<IEnumerable<TEntity>> GetPropertiesAsync(Expression<Func<TEntity, TEntity>> select,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        int? take = null,
        int? skip = null);

    Task<TEntity?> GetByIdAsync(int id);

    Task<IEnumerable<TEntity?>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "");

    Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null);
    Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
    Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
    Task<TEntity?> GetSingleOrDefaultAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
    Task<TEntity?> GetSingleOrDefaultAsTrackingAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
}
