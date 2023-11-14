namespace ServiceCache;

public interface ICacheService
{
    Task<T> CreateAndSetAsync<T>(string key, Func<Task<T>> createAsync);
    Task<T> GetOrDefault<T>(string key, T defaultVal);
}
