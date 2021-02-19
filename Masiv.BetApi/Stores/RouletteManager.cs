using Masiv.BetApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Masiv.BetApi.Stores
{
    public class RouletteManager<TRoulette> : IDisposable where TRoulette : class
    {
        protected virtual CancellationToken CancellationToken => CancellationToken.None;
        protected internal IRouletteStore<TRoulette> Store { get; set; }
        public RouletteManager(IRouletteStore<TRoulette> store)
        {
            Store = store ?? throw new ArgumentNullException(nameof(store));
        }
        public virtual async Task<TRoulette> FindByIdAsync(TRoulette roulette)
        {
            if (roulette == null)
                throw new ArgumentNullException(nameof(roulette));
            return await Store.FindByIdAsync(roulette);
        }
        public virtual async Task<Response<TRoulette>> CreateAsync(TRoulette roulette)
        {
            if (roulette == null)
                throw new ArgumentNullException(nameof(roulette));
            return await Store.CreateAsync(roulette);
        }
        public virtual async Task<Response> UpdateAsync(TRoulette roulette)
        {
            if (roulette == null)
                throw new ArgumentNullException(nameof(roulette));
            return await Store.UpdateAsync(roulette);
        }
        public virtual async Task<List<TRoulette>> GetAllAsync()
        {
            return await Store.GetAllAsync();
        }
        public void Dispose()
        {
        }
    }
}
