using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class CustomerBasketRepository : ICustomerBasketRepository
    {
        private readonly IDatabase _redisDb;
        public CustomerBasketRepository(IConnectionMultiplexer redis)
        {
            _redisDb = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _redisDb.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
           var result = await _redisDb.StringGetAsync(basketId);
            if (!result.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<CustomerBasket>(result!);

            }
            return null;
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var result = _redisDb.StringSet(basket.Id,JsonSerializer.Serialize(basket));
            if (result)
            {
                return await GetBasketAsync(basket.Id);
            }
            return null;
        }
    }
}
