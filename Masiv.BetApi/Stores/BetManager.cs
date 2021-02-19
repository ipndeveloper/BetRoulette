using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Masiv.BetApi.Models;

namespace Masiv.BetApi.Stores
{
    public class BetManager<TBet> : IDisposable where TBet : class
    {
        protected virtual CancellationToken CancellationToken => CancellationToken.None;
        protected internal IBetStore<TBet> Store { get; set; }
        public BetManager(IBetStore<TBet> store)
        {
            if (store == null)
            {
                throw new ArgumentNullException(nameof(store));
            }
            Store = store;
        }
        public virtual async Task<Response<TBet>> CreateAsync(TBet user)
        {
            return await Store.CreateAsync(user);
        }
        public virtual async Task<List<TBet>> GetAllByRouletteIdAsync(TBet bet)
        {
            return await Store.GetAllByRouletteIdAsync(bet);
        }
        public void Dispose()
        {
            
        }
    }
}
