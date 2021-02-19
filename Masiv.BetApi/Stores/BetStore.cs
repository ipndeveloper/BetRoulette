using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Masiv.BetApi.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace Masiv.BetApi.Stores
{
    public class BetStore : IBetStore<Bet>

    {
        private readonly IRedisCacheClient _cache;
        private string _redisPattern = "BETS";
        public BetStore(IRedisCacheClient cache)
        {
            _cache = cache;
        }
        public async Task<Response<Bet>> CreateAsync(Bet bet)
        {
            if (bet == null)
            {
                throw new ArgumentNullException(nameof(bet));
            } 
            var bets =  await  this.GetAllBetsAsync(bet);
            bets.Add(bet);
            await _cache.Db0.HashSetAsync($"{_redisPattern}:{bet.RouletteId}", bet.RouletteId, JsonConvert.SerializeObject(bets));
            return new Response<Bet> { Result = bet , Succeeded = true};
        }
        public void Dispose()
        {
        }
        public  Task<List<Bet>> GetAllByRouletteIdAsync(Bet bet)
        {
            if (bet == null)
            {
                throw new ArgumentNullException(nameof(bet));
            }
            return  GetAllBetsAsync(bet);
        }
        private async Task<List<Bet>> GetAllBetsAsync(Bet bet)
        {
            if (bet == null)
            {
                throw new ArgumentNullException(nameof(bet));
            }
            List<Bet> bets = new List<Bet>();
            var response = await _cache.Db0.HashGetAllAsync<string>($"{_redisPattern}:{bet.RouletteId}");
            if (response.Values.Count > 0)
            {
                string rouletteHashValue = response.Values.FirstOrDefault() ?? string.Empty;
                bets = JsonConvert.DeserializeObject<List<Bet>>(rouletteHashValue);
            }
            return bets;
        }
      
    }
}
