using Application.Travel.Interfaces;
using Application.Travel.Services;
using Domain.Travel.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using OfficeOpenXml.Filter;
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

            var cachedData = await _cacheRepository.HashGetAllAsync(_key);

            var dataList = new List<T>();
            foreach (var hashEntry in cachedData)
            {
                var jsonString = hashEntry.Value.ToString();
                var data = JsonSerializer.Deserialize<T>(jsonString);
                dataList.Add(data);
            }
            return dataList;
        }


        //eklerken dbye ekle sadece redise doğrudan ekleme yapma
        public async Task<T> AddAsync(T entity)
        {

            var data = await _repository.AddAsync(entity);

          
            return data;
        }
       

        private async Task<int> GetMaxIdFromCacheAsync()
        {
            
            var cacheData = await _cacheRepository.HashGetAllAsync(_key);
            int maxId = cacheData.Select(item => int.Parse(item.Key)).Max();
            return maxId;
        }


        public async Task<T> FindAsync(int id)
        {
            var entityId = id.ToString();


            var cachedData = await _cacheRepository.HashGetAsync(_key, entityId);
            if (cachedData.HasValue)
            {
                return JsonSerializer.Deserialize<T>(cachedData);
            }
            else
            {
                var data = await _repository.FindAsync(id);
                if (data != null)
                {
                    await _cacheRepository.HashSetAsync(_key, entityId, JsonSerializer.Serialize(data));
                }
                return data;
            }
        }



     
        public async Task<T> GetByIdAsync(int id)
        {
            var entityId = id.ToString();

            var cachedData = await _cacheRepository.HashGetAsync(_key, entityId);
            if (cachedData.HasValue)
            {
                return JsonSerializer.Deserialize<T>(cachedData);
            }
            else
            {
                var data = await _repository.GetByIdAsync(id); 
                {
                    await _cacheRepository.HashSetAsync(_key, entityId, JsonSerializer.Serialize(data));
                }
                return data;
            }
        }

        public List<T> GetList(Expression<Func<T, bool>> filter)
        {
            var cacheKey = $"{_key}";
            var cachedData = _cacheRepository.HashGet(_key, cacheKey);

            if (cachedData.HasValue)
            {
                var cachedResult = JsonSerializer.Deserialize<List<T>>(cachedData);
                var hasMatchingDataInCache = cachedResult.Any(filter.Compile());

                if (hasMatchingDataInCache)
                    return cachedResult.Where(filter.Compile()).ToList();
            }

           
            var result = _repository.GetList(filter);
            _cacheRepository.HashSet(_key, cacheKey, JsonSerializer.Serialize(result));
            return result;
        }


        public IQueryable<T> GetQuery()
        {
            return _repository.GetQuery();
        }

        public async Task Delete(T entity)
        {
            PropertyInfo prop = typeof(T).GetProperty("Id");
            var entityId = prop.GetValue(entity).ToString();

            var existsInCache = await _cacheRepository.HashExistsAsync(_key, entityId);
         
                if (existsInCache)
                {
                    await _cacheRepository.HashDeleteAsync(_key, entityId);
                     _repository.Delete(entity);

                }
                else
                {

                     _repository.Delete(entity);
                }
            
        }

        public async Task Update(T t, T unchanged)
        {

            var serializeObject = JsonSerializer.Serialize(t);
            PropertyInfo prop = typeof(T).GetProperty("Id");
            var entityId = prop.GetValue(t).ToString();
            if (entityId != null)
            {
                await _cacheRepository.HashSetAsync(_key, entityId, serializeObject);
                _repository.Update(t, unchanged);
            }
            else
            {
                _repository.Update(t, unchanged);
            }
            
        }


        private async Task<List<T>> LoadToCacheFromDbAsync()
        {
            var data = await _repository.GetListAsync();
            data.ForEach(x =>
            {
                PropertyInfo prop = typeof(T).GetProperty("Id");
                var idValue = prop.GetValue(x);
                _cacheRepository.HashSetAsync(_key, idValue.ToString(), JsonSerializer.Serialize(x));
            });

            return data;
        }
      
        public T GetByFilter(Expression<Func<T, bool>> filter = null)
        {
            var cacheKey = $"{_key}:{filter}";

            var cachedData = _cacheRepository.HashGet(_key, cacheKey);
            if (cachedData.HasValue)
            {
                return JsonSerializer.Deserialize<T>(cachedData);
            }
            else
            {
                var result = _repository.GetByFilter(filter);
                _cacheRepository.HashSet(_key, cacheKey, JsonSerializer.Serialize(result));
                return result;
            }
        }

    
    }
}
