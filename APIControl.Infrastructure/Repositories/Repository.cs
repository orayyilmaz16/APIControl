// APIControl.Infrastructure/Repositories/Repository.cs
using Microsoft.EntityFrameworkCore;
using APIControl.Domain.Abstractions;
using System.Linq.Expressions;

namespace APIControl.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _ctx;
    private readonly DbSet<T> _set;
    public Repository(DbContext ctx) { _ctx = ctx; _set = ctx.Set<T>(); }

    public async Task<T?> GetByIdAsync(Guid id) => await _set.FindAsync(id);
    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null)
        => predicate is null ? await _set.AsNoTracking().ToListAsync()
                             : await _set.AsNoTracking().Where(predicate).ToListAsync();

    public async Task AddAsync(T entity) => await _set.AddAsync(entity);
    public void Update(T entity) => _set.Update(entity);
    public void Remove(T entity) => _set.Remove(entity);
}
