using System.Reflection;
using UrlShortenerService.Application.Common.Interfaces;
using UrlShortenerService.Infrastructure.Persistence.Interceptors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UrlShortenerService.Domain.Entities;
using UrlShortenerService.Infrastructure.Common;

namespace UrlShortenerService.Infrastructure.Persistence;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IMediator mediator,
    AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Url> Urls => Set<Url>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        _ = builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _ = optionsBuilder.AddInterceptors(auditableEntitySaveChangesInterceptor);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await mediator.DispatchDomainEvents(this);

        return await base.SaveChangesAsync(cancellationToken);
    }
}
