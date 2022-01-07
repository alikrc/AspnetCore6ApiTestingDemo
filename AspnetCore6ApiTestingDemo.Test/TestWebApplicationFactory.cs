using AspnetCore6ApiTestingDemo.Infra;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;

namespace AspnetCore6ApiTestingDemo.Test;

public sealed class TestWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint> where TEntryPoint : class
{
    public readonly static string ConnectionString = "Data Source=TestDb.db";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            RemoveAllDbContextsFromServices(services);

            services.AddDbContext<DemoContext>(options =>
            {
                var projectAssemblyName = Assembly.GetAssembly(typeof(TestWebApplicationFactory<>)).GetName().Name;
                options.UseSqlite(ConnectionString, x => x.MigrationsAssembly(projectAssemblyName));
            });

            services.AddDbContext<DemoContextSqlite>();

            MigrateDbContext<DemoContextSqlite>(services);
        });
    }

    private void RemoveAllDbContextsFromServices(IServiceCollection services)
    {
        // reverse operation of AddDbContext<XDbContext> which removes  DbContexts from services
        var descriptors = services.Where(d => d.ServiceType.BaseType == typeof(DbContextOptions)).ToList();
        descriptors.ForEach(d => services.Remove(d));

        var dbContextDescriptors = services.Where(d => d.ServiceType.BaseType == typeof(DbContext)).ToList();
        dbContextDescriptors.ForEach(d => services.Remove(d));
    }

    public void MigrateDbContext<TContext>(IServiceCollection serviceCollection) where TContext : DbContext
    {
        var serviceProvider = serviceCollection.BuildServiceProvider();

        using var scope = serviceProvider.CreateScope();

        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetService<TContext>();

        try
        {
            logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

            if (context.Database.IsSqlServer())
            {
                throw new Exception("Use Sqlite instead of sql server!");
            }

            context.Database.EnsureDeleted();

            context.Database.Migrate();

            logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
            throw;
        }
    }
}