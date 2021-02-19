using Masiv.BetApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Masiv.BetApi.Utilty;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis;

namespace Masiv.BetApi.Stores
{
    public class RouletteStore : IRouletteStore<Roulette>
    {
        private readonly string redisNameKey = "ROULETTES";
        private readonly IRedisCacheClient _cache;

        public RouletteStore(IRedisCacheClient  cache)
        {
            _cache = cache;
        }
        public async Task<Response<Roulette>> CreateAsync(Roulette roulette)
        {
            if (roulette == null)
            {
                throw new ArgumentNullException(nameof(roulette));
            }
            Roulette rouletteCreated = await CreateAsync(roulette, true);
            return  new Response<Roulette> {  Result  = rouletteCreated , Succeeded = true , Message ="La rouleta se registro correctamente."};
        }

        public async Task<Roulette> FindByIdAsync(Roulette roulette)
        {
            if (roulette == null)
            {
                throw new ArgumentNullException(nameof(roulette));
            }
            List<Roulette> roulettes = await this.GetAllRouletteAsync();
            return roulettes.FirstOrDefault(x => x.RouletteId == roulette.RouletteId) ;
        }
        public async Task<Response> UpdateAsync(Roulette roulette)
        {
            if (roulette == null)
            {
                throw new ArgumentNullException(nameof(roulette));
            } 
            await CreateAsync(roulette, false);
            return Response.Success;

        }
        public Task<List<Roulette>> GetAllAsync()
        {
            return this.GetAllRouletteAsync();
        }
        public void Dispose()
        {
        }
        internal async Task<Roulette> CreateAsync(Roulette roulette , bool create )
        {
            List<Roulette>  roulettes = await this.GetAllRouletteAsync();
            if(create)
               roulettes.Add(roulette);
            else
            { 
                roulettes = roulettes.Where(x => x.RouletteId != roulette.RouletteId).ToList();
                roulettes.Add(roulette);
            }
            await _cache.Db0.AddAsync<string>(redisNameKey, JsonConvert.SerializeObject(roulettes));
            return roulette;
        }
        private async Task<List<Roulette>> GetAllRouletteAsync()
        {
            var roulettes = await _cache.Db0.GetAsync<string>( redisNameKey ) ?? string.Empty;
            return JsonConvert.DeserializeObject<List<Roulette>>(roulettes) ?? new List<Roulette>();
        }
    }
}
