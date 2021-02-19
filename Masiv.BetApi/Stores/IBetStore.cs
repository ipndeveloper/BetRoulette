using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Masiv.BetApi.Models;

namespace Masiv.BetApi.Stores
{
    public interface IBetStore<TBet> : IDisposable where TBet : class
    {
        Task<Response<TBet>> CreateAsync(TBet bet);
        Task<List<TBet>> GetAllByRouletteIdAsync(TBet bet);
    }
}
