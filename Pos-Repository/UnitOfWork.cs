using Microsoft.Extensions.DependencyInjection;
using Pos_Repository.Repositories;

namespace Pos_Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    private readonly IServiceProvider serviceProvider;
    private readonly Dictionary<Type, object> repositories;

    public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
    {
        this.context = context;
        this.serviceProvider = serviceProvider;
        repositories = new Dictionary<Type, object>();
    }

    public TCustomRepository? GetCustomRepository<TCustomRepository>() where TCustomRepository : class
    {
        var repositoryType = typeof(TCustomRepository);

        if (repositories.ContainsKey(repositoryType))
        {
            return repositories[repositoryType] as TCustomRepository;
        }

        var customRepositoryInstance = serviceProvider.GetRequiredService<TCustomRepository>();

        repositories.Add(repositoryType, customRepositoryInstance);

        return customRepositoryInstance;
    }

    public IRepository<TEntity>? GetRepositoryFor<TEntity>() where TEntity : class
    {
        var repositoryType = typeof(IRepository<TEntity>);

        if (repositories.ContainsKey(repositoryType))
        {
            return repositories[repositoryType] as IRepository<TEntity>;
        }

        var repositoryInstance = new Repository<TEntity>(context);

        repositories.Add(repositoryType, repositoryInstance);

        return repositoryInstance;
    }

    public async Task<int> SaveChangesAsync()
    {
        var entriesWrittenCount = await context.SaveChangesAsync();
        return entriesWrittenCount;
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
