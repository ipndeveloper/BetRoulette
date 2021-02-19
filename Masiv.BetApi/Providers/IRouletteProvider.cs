using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masiv.BetApi.Models;

namespace Masiv.BetApi.Providers
{
    public interface IRouletteProvider
    {
        Task<Response<Roulette>> CreateAsync(Roulette roulette);
        Task<List<Roulette>> GetAllAsync();
        Task<Response<Bet>> AddToBetAsync(Bet bet);
        Task<Response> OpenAsync(Roulette roulette);
        Task<Response<List<BetResult>>> CloseAsync(Roulette roulette);
    }
}
