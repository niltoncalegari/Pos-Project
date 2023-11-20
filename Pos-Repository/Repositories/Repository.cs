using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Pos_Repository.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly AppDbContext context;
    private readonly DbSet<TEntity> dbSet;

    public Repository(AppDbContext context)
    {
        this.context = context;
        dbSet = context.Set<TEntity>();
    }

    public void Add(TEntity entity)
    {
        dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        dbSet.AddRange(entities);
    }

    public void Update(TEntity entity)
    {
        dbSet.Update(entity);
    }

    public void Remove(TEntity entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        dbSet.RemoveRange(entities);
    }

    public void Attach<T>(T entity)
        where T : class
    {
        context.Attach(entity);
    }

    public void Detach<T>(T entity)
        where T : class
    {
        context.Entry(entity).State = EntityState.Detached;
    }

    public void SetIsModified<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty?>> propertyExpression)
        where TProperty : class
    {
        var entry = context.Entry(entity);
        entry.Reference(propertyExpression).IsModified = true;
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        int? take = null,
        int? skip = null)
    {
        IQueryable<TEntity> query = Filter(dbSet, filter);

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await Paginate(orderBy(query), take, skip).ToListAsync();
        }

        return await Paginate(query, take, skip).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAsNoTrackingAsync(Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        int? take = null,
        int? skip = null)
    {
        IQueryable<TEntity> query = FilterAsNotracking(dbSet, filter);

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.AsNoTracking().Include(includeProperty);
        }

        if (orderBy != null)
        {
            return await Paginate(orderBy(query), take, skip).ToListAsync();
        }

        return await Paginate(query, take, skip).ToListAsync();
    }

    public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
    {
        IQueryable<TEntity> query = Filter(dbSet, filter);
        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        return await query.SingleAsync();
    }

    public async Task<IEnumerable<TEntity>> GetPropertiesAsync(Expression<Func<TEntity, TEntity>> select,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        int? take = null,
        int? skip = null)
    {
        IQueryable<TEntity> query = Filter(dbSet, filter);

        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        query = query.Select(select);

        if (orderBy != null)
        {
            return await Paginate(orderBy(query), take, skip).ToListAsync();
        }

        return await Paginate(query, take, skip).ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = Filter(dbSet, filter);

        return await Paginate(query).CountAsync();
    }

    private IQueryable<TEntity> Filter(IQueryable<TEntity> query, Expression<Func<TEntity, bool>>? filter = null)
    {
        if (filter != null)
            query = query.Where(filter);

        return query;
    }

    private IQueryable<TEntity> FilterAsNotracking(IQueryable<TEntity> query, Expression<Func<TEntity, bool>>? filter = null)
    {
        if (filter != null)
            query = query.AsNoTracking().Where(filter);

        return query;
    }

    internal DbSet<TEntity> GetDbSet() => dbSet;

    private IQueryable<TEntity> Paginate(IQueryable<TEntity> query, int? top = null, int? skip = null)
    {
        if (skip.HasValue)
            query = query.Skip(skip.Value);
        if (top.HasValue)
            query = query.Take(top.Value);
        return query;
    }

    public async Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
    {
        IQueryable<TEntity> query = Filter(dbSet, filter);
        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        return await query.FirstOrDefaultAsync();
    }

    public async Task<TEntity?> GetSingleOrDefaultAsNoTrackingAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
    {
        IQueryable<TEntity> query = Filter(dbSet, filter);
        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }
        return await query.AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity?>> GetAllByFilterAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
    {
        IQueryable<TEntity> query = Filter(dbSet, filter);
        foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        return query.AsEnumerable();
    }

    public Task<IEnumerable<TEntity>> GetAsTrackingAsync(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, string includeProperties = "", int? take = null, int? skip = null)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetSingleOrDefaultAsTrackingAsync(Expression<Func<TEntity, bool>> filter, string includeProperties = "")
    {
        throw new NotImplementedException();
    }
}
