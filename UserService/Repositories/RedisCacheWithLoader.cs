using System.Text.Json;
using StackExchange.Redis;

namespace UserService.Repositories.Interface;

public class RedisCacheWithLoader<TKey, TValue>
{
   private readonly IDatabase _redis;
   // private readonly Func<TKey, string> _keyFormatter;
   private readonly ICacheLoader<TKey, TValue> _loader;
   private readonly TimeSpan _expiry;

   public RedisCacheWithLoader(
      IDatabase redis,
      ICacheLoader<TKey, TValue> loader,
      TimeSpan? expiry = null)
   {
      _redis = redis;
      _loader = loader;
      _expiry = expiry ?? TimeSpan.FromMinutes(10);
   }

   public async Task<TValue?> GetAsync(TKey key, Func<TKey, string> keyFormatter)
   {
      
      string redisKey = keyFormatter(key);
      string? cached = await _redis.StringGetAsync(redisKey);
      if (!string.IsNullOrEmpty(cached))
      {
         return JsonSerializer.Deserialize<TValue>(cached);
      }

      TValue? value = await _loader.LoadAsync(key);
      if (value != null)
      {
         string json = JsonSerializer.Serialize(value);
         await _redis.StringSetAsync(redisKey, json, _expiry);
      }

      return value;
   }
}