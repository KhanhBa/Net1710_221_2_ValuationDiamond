using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using ValuationDiamond.Data.Models;
using IDatabase = StackExchange.Redis.IDatabase;

public class RedisManagement
{
    private readonly IDatabase _cache;
    private readonly ConnectionMultiplexer _redis;

    public RedisManagement()
    {
        _redis = ConnectionMultiplexer.Connect("outfit4rent.online:6379");
        _cache = _redis.GetDatabase();
    }

    public RedisManagement(ConnectionMultiplexer redis)
    {
        _redis = redis;
        _cache = _redis.GetDatabase();
    }

    public void SetData(string key, string value)
    {
        _cache.StringSet(key, value, TimeSpan.FromMinutes(30));
    }


    public string GetData(string key)
    {
        return _cache.StringGet(key);
    }

    public void DeleteData(string key)
    {
        _cache.KeyDelete(key);
    }
    public void Dispose()
    {
        _redis?.Dispose();
    }
    public async Task AddValuationCertificateToListAsync(string key, ValuationCertificate product)
    {
        var products = await GetValuationCertificatesFromListAsync(key);
        products.Add(product);
        var productJson = JsonConvert.SerializeObject(products);
        await _cache.StringSetAsync(key, productJson);
    }

    public async Task<List<ValuationCertificate>> GetValuationCertificatesFromListAsync(string key)
    {
        var productList = new List<ValuationCertificate>();
        var productJson = await _cache.StringGetAsync(key);

        if (!string.IsNullOrEmpty(productJson))
        {
            productList = JsonConvert.DeserializeObject<List<ValuationCertificate>>(productJson);
        }
        return productList;
    }

    public async Task PublishAsync(string channel, string message)
    {
        var sub = _redis.GetSubscriber();
        await sub.PublishAsync(channel, message);
    }
}
