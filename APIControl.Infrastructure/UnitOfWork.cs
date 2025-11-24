// APIControl.Infrastructure/UnitOfWork.cs
using APIControl.Domain.Abstractions;
using APIControl.Infrastructure.Data;

namespace APIControl.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _ctx;
    private readonly Dictionary<Type, object> _repos = new();

    public UnitOfWork(AppDbContext ctx) => _ctx = ctx;

    public IRepository<T> Repository<T>() where T : class
    {
        var type = typeof(T);
        if (!_repos.TryGetValue(type, out var repo))
        {
            repo = new Repositories.Repository<T>(_ctx);
            _repos[type] = repo;
        }
        return (IRepository<T>)repo;
    }

    public Task<int> SaveChangesAsync(CancellationToken ct = default)
        => _ctx.SaveChangesAsync(ct);

    public ValueTask DisposeAsync() => _ctx.DisposeAsync();
}
