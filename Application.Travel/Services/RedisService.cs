
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.Travel.Services
{
    public class RedisService
    {
        private readonly string _redisHost;
        private readonly string _redisPort;
        private ConnectionMultiplexer _redis;
        public IDatabase db { get; set; }
        public RedisService(IConfiguration configuration)
        {
            _redisHost = configuration["Redis:Host"];
            _redisPort = configuration["Redis:Port"];
        }

        public void Connect()
        {
            var configOptions = new ConfigurationOptions
            {
                EndPoints = { $"{_redisHost}:{_redisPort}" },
                AbortOnConnectFail = false, // Bağlantı hatası durumunda yeniden denemeyi sağlar
                ConnectTimeout = 5000, // Bağlantı zaman aşımı
                SyncTimeout = 5000 // Senkronize etme zaman aşımı
            };

            _redis = ConnectionMultiplexer.Connect(configOptions);
        }


        public IDatabase GetDb(int db) 
        {
            return _redis.GetDatabase(db);  
        }
    }
}
