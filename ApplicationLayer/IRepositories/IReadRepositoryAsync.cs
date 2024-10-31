using Domain.Contracts;
namespace ApplicationLayer.IRepositories;
public interface IReadRepositoryAsync<T, in TId> where T : class, IEntity<TId>
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(TId id);
    Task<T> LoginAsync(string username, string password);
    IQueryable<T> Entities { get; }
}