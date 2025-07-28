namespace UserService.Repositories.Interface;

public interface ICacheLoader<TKey, TValue>
{
   Task<TValue?> LoadAsync(TKey key);
}