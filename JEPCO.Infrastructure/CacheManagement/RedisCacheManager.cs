//using JEPCO.Application.Interfaces.CacheManagement;
//using StackExchange.Redis;
//using System.Text.Json;

//namespace JEPCO.Infrastructure.CacheManagement
//{
//    public class RedisCacheManager : ICacheManager
//    {
//        private IDatabase cacheDb;
//        public RedisCacheManager(IConnectionMultiplexer connectionMultiplexer)
//        {
//            cacheDb = connectionMultiplexer.GetDatabase();
//        }
//        /// <summary>
//        /// Get Data using key
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public T GetData<T>(string cacheKey)
//        {
//            var value = cacheDb.StringGet(cacheKey);
//            if (!string.IsNullOrEmpty(value))
//            {
//                return JsonSerializer.Deserialize<T>(value);
//            }
//            return default;
//        }

//        /// <summary>
//        /// Set Data with Value and Exxpirty Duration (in minutes) of Key
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <param name="expirtyDuration"></param>
//        /// <returns></returns>
//        public bool SetData<T>(string cacheKey, T value, int expirtyDuration)
//        {
//            var ts = new TimeSpan(hours: 0, minutes: expirtyDuration, seconds: 0);
//            return cacheDb.StringSet(cacheKey, JsonSerializer.Serialize<T>(value), ts);
//        }

//        /// <summary>
//        /// Remove Data
//        /// </summary>
//        /// <param name="key"></param>
//        /// <returns></returns>
//        public object RemoveData(string key)
//        {
//            var isExist = cacheDb.KeyExists(key);
//            if (!isExist)
//            {
//                return cacheDb.KeyDelete(key);
//            }
//            return false;
//        }

//    }
//}
