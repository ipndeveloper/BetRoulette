using Masiv.BetApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Masiv.BetApi.Stores
{
    public interface IRouletteStore<TRoulette> : IDisposable where TRoulette : class
    {
        Task<Response<TRoulette>> CreateAsync(TRoulette roulette);
        Task<TRoulette> FindByIdAsync(TRoulette roulette);
        Task<List<TRoulette>> GetAllAsync();
        Task<Response> UpdateAsync(TRoulette roulette);
    }
}
