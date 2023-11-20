using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pos_Service.Common;

namespace Pos_Repository;

public class AppDbContextFactory : DesignTimeDbContextFactoryBase<AppDbContext>
{
    protected override AppDbContext CreateNewInstance(DbContextOptionsBuilder<AppDbContext> builder)
    {
        return new AppDbContext(builder.Options);
    }
}

public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
{
    private const string ConnectionStringName = "AppDatabase";

    public TContext CreateDbContext(string[] args)
    {
        var basePath = Directory.GetCurrentDirectory() + string.Format("{0}..{0}Pos-Project", Path.DirectorySeparatorChar);
        return Create(basePath, AppEnvironment.GetCurrentEnvironment());
    }

    protected abstract TContext CreateNewInstance(DbContextOptionsBuilder<TContext> builder);

    private TContext Create(string basePath, string environmentName)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.Local.json", optional: true)
            .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString(ConnectionStringName);

        if (string.IsNullOrEmpty(connectionString))
            throw new ArgumentException($"Connection string '{ConnectionStringName}' is null or empty.", nameof(connectionString));

        Console.WriteLine($"DesignTimeDbContextFactoryBase.Create(string): Connection string: '{connectionString}'.");

        return CreateNewInstance(GetBuilder(connectionString));
    }

    protected virtual DbContextOptionsBuilder<TContext> GetBuilder(string connectionString)
    {
        var builder = new DbContextOptionsBuilder<TContext>();
        builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

        return builder;
    }
}
