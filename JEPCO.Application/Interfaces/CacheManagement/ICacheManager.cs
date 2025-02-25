namespace JEPCO.Application.Interfaces.CacheManagement;

public interface ICacheManager
{
    T GetData<T>(string cacheKey);
    bool SetData<T>(string cacheKey, T value, int expirtyDuration);
    object RemoveData(string key);
}
