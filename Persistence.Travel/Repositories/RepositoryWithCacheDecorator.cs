using Application.Travel.Interfaces;
using Application.Travel.Services;
using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Travel.Repositories
{
    public class RepositoryWithCacheDecorator<T> : IRepository<T> where T : class
    {
        private readonly RedisService _redisService;
        private readonly IRepository<T> _repository;
        private readonly string _key;
        private readonly StackExchange.Redis.IDatabase _cacheRepository;

        //decorative design pattern
        public RepositoryWithCacheDecorator(RedisService redisService,
            IRepository<T> repository)
        {
            _redisService = redisService;
            _repository = repository;
            _key = $"{typeof(T).Name}Caches";
            _cacheRepository = _redisService.GetDb(0);
        }


        public async Task<List<T>> GetListAsync()
        {
            
            if (!await _cacheRepository.KeyExistsAsync(_key))
                return await LoadToCacheFromDbAsync();

            var cachedData = await _cacheRepository.StringGetAsync(_key);

            if (string.IsNullOrEmpty(cachedData))
                return await LoadToCacheFromDbAsync();

            var dataList = JsonSerializer.Deserialize<List<T>>(cachedData);

            return dataList;
        }


        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);

            var serializeObject = JsonSerializer.Serialize(entity);
            PropertyInfo prop = typeof(T).GetProperty("Id");
            var entityId = prop.GetValue(entity).ToString();

            if (await _cacheRepository.KeyExistsAsync(_key))
            {
                await _cacheRepository.KeyDeleteAsync(_key);
            }

            await _cacheRepository.StringSetAsync(_key, serializeObject);
        }


        public async Task<T> FindAsync(object id)
        {
            var entityId = id.ToString();

            var cachedData = await _cacheRepository.StringGetAsync($"{_key}:{entityId}");

            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonSerializer.Deserialize<T>(cachedData);
            }
            else
            {
                var data = await _repository.FindAsync(id);
                if (data != null)
                {
                    await _cacheRepository.StringSetAsync($"{_key}:{entityId}", JsonSerializer.Serialize(data));
                }
                return data;
            }
        }




        public async Task<T> GetByIdAsync(int id)
        {
            var entityId = id.ToString();

            if (await _cacheRepository.KeyExistsAsync(_key))
            {
                var data = await _cacheRepository.StringGetAsync($"{_key}:{entityId}");
                return !string.IsNullOrEmpty(data) ? JsonSerializer.Deserialize<T>(data) : default;
            }

            var dbData = await LoadToCacheFromDbAsync();
            PropertyInfo prop = typeof(T).GetProperty("Id");
            var datas = dbData.FirstOrDefault(x => prop.GetValue(x).ToString() == entityId);
            return datas;

        }


        public T GetList(Expression<Func<T, bool>> filter)
        {
            var cacheKey = $"{_key}:{filter}";

            var cachedData =  _cacheRepository.StringGetAsync($"{_key}:{cacheKey}");
            if (cachedData!=null)
            {
                var resultString = cachedData.ToString();
                return JsonSerializer.Deserialize<T>(resultString);
            }
            else
            {
                var result = _repository.GetList(filter);
                 _cacheRepository.StringSetAsync($"{_key}:{cacheKey}", JsonSerializer.Serialize(result));
                return result;
            }
        }



        public IQueryable<T> GetQuery()
        {
            return _repository.GetQuery();
        }

        public async void Delete(T entity)
        {
            PropertyInfo prop = typeof(T).GetProperty("Id");
            var entityId = prop.GetValue(entity).ToString();

            var existsInCache = await _cacheRepository.KeyExistsAsync($"{_key}:{entityId}");

            if (existsInCache)
            {
               await  _cacheRepository.KeyDeleteAsync($"{_key}:{entityId}");
                _repository.Delete(entity);
            }
            else
            {
                _repository.Delete(entity);
            }
        }

        public void Update(T t, T unchanged)
        {
            _repository.Update(t, unchanged);

            var serializeObject = JsonSerializer.Serialize(t);
            PropertyInfo prop = typeof(T).GetProperty("Id");
            var entityId = prop.GetValue(t).ToString();
             _cacheRepository.StringSetAsync($"{_key}:{entityId}", serializeObject);
        }



        private async Task<List<T>> LoadToCacheFromDbAsync()
        {
            var data = await _repository.GetListAsync();
            var tasks = new List<Task>();

            foreach (var item in data)
            {
                PropertyInfo prop = typeof(T).GetProperty("Id");
                var idValue = prop.GetValue(item);

                var jsonString = JsonSerializer.Serialize(item);
                tasks.Add(_cacheRepository.StringSetAsync($"{_key}:{idValue}", jsonString));
            }

            await Task.WhenAll(tasks);

            return data;
        }

        


    }
}
