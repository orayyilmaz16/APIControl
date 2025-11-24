using System;
using System.Collections.Generic;
using System.Text;

namespace APIControl.Domain.Abstractions
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
