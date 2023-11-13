namespace ServiceCache
{
	public interface ICacheService
	{
		void ClearCache();
		Task CreateAndSet<T>(string key, T thing, int expirationMinutes = 0) where T : class;
		Task<T> CreateAndSetAsync<T>(string key, Func<Task<T>> createAsync, int expirationMinutes = 0);
		Task<T> CreateAndSetParalelAsync<T>(string key, Func<Task<T>> createAsync, int expirationMinutes = 0);
		Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> create, int expirationMinutes = 0);
		Task<T> GetOrCreateParallelAsync<T>(string key, Func<Task<T>> create, int expirationMinutes = 0);
		Task<T> GetOrDefault<T>(string key);
        Task<T> GetOrDefault<T>(string key, T defaultVal);
        Task Remove(string key);
	}

}
