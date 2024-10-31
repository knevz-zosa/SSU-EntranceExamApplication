using ApplicationLayer.IRepositories;
using Common.Security;
using DataAccessLayer.Context;
using Domain.Contracts;
using System.Collections;

namespace DataAccessLayer.Repositories;
public class UnitOfWork<TId> : IUnitOfWork<TId>
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private bool disposed;
    private Hashtable _repositories;
    public UnitOfWork(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }
    public async Task<int> CommitAsync(CancellationToken cancellationToken)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public IReadRepositoryAsync<T, TId> ReadRepositoryFor<T>() where T : BaseEntity<TId>
    {
        if (_repositories == null)
        {
            _repositories = new Hashtable();
        }

        var type = $"{typeof(T).Name}_Read";

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(ReadRepositoryAsync<,>);
            var repositoryInstance = Activator
                .CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)),
                _context,
                _passwordHasher
                );
            _repositories.Add(type, repositoryInstance);
        }

        return (IReadRepositoryAsync<T, TId>)_repositories[type];
    }

    public IWriteRepositoryAsync<T, TId> WriteRepositoryFor<T>() where T : BaseEntity<TId>
    {
        if (_repositories == null)
        {
            _repositories = new Hashtable();
        }

        var type = $"{typeof(T).Name}_Write";

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(WriteRepositoryAsync<,>);
            var repositoryInstance = Activator
                .CreateInstance(repositoryType.MakeGenericType(typeof(T), typeof(TId)), _context);

            _repositories.Add(type, repositoryInstance);
        }

        return (IWriteRepositoryAsync<T, TId>)_repositories[type]; ;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        disposed = true;
    }
}
