using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace RedisAPI
{
    public class RedisConnectionFactory
    {
        private static readonly Lazy<ConnectionMultiplexer> _connection;

        private static readonly string REDIS_CONNECTIONSTRING = "REDIS_CONNECTIONSTRING";

        static RedisConnectionFactory()

        {
            _connection = new Lazy<ConnectionMultiplexer>(() => 
            { 
                return ConnectionMultiplexer.Connect("192.168.1.66:6379");
            });
        }

        public static ConnectionMultiplexer GetConnection() => _connection.Value;
    }

    public class RedisDataAgent
    {
        private static IDatabase _database;
        private static int num = 0;
        public RedisDataAgent()
        {
            var connection = RedisConnectionFactory.GetConnection();

            _database = connection.GetDatabase();
            num++;
        }

        public string GetStringValue(string key)
        {
            return _database.StringGet(key);
        }


        public void SetStringValue(string key, string value)
        {
            _database.StringSet(key, value, TimeSpan.FromMinutes(1));
        }
        public T Get<T>(string key)
        {
            var cacheValue =_database.StringGet(key);
            T deserializedCache = default(T);
            if (!string.IsNullOrEmpty(cacheValue)) 
            {
                deserializedCache = JsonConvert.DeserializeAnonymousType<T>(cacheValue, deserializedCache);
            }
            return deserializedCache;
        }

        public void Set<T>(string key, T value)
        {
            var cacheString = JsonConvert.SerializeObject(value);
            var timeout = TimeSpan.FromMinutes(6);
            _database.StringSet(key, cacheString, timeout);
        }

        public void DeleteStringValue(string key)
        {
            _database.KeyDelete(key);
        }


    }
}
