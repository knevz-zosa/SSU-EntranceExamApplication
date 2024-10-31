using ApplicationLayer.IRepositories;
using Common.Security;
using DataAccessLayer.Context;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories;

public class ReadRepositoryAsync<T, TId> : IReadRepositoryAsync<T, TId> where T : BaseEntity<TId>
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public ReadRepositoryAsync(ApplicationDbContext context, IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public IQueryable<T> Entities => _context.Set<T>();

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> GetAsync(TId id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> LoginAsync(string username, string password)
    {
        var user = await _context.Set<T>()
            .Where(u => EF.Property<string>(u, "Username") == username)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            throw new UserNotFoundException();
        }

        var storedPasswordHash = EF.Property<string>(user, "PasswordHash");

        if (!_passwordHasher.VerifyPassword(storedPasswordHash, password))
        {
            throw new InvalidPasswordException(); 
        }

        return user; 
    }
}

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("User not found") { }
}

public class InvalidPasswordException : Exception
{
    public InvalidPasswordException() : base("Invalid password") { }
}
