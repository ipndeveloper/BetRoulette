using Masiv.BetApi.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masiv.BetApi.Commons;
using Masiv.BetApi.Commons.Enums;
using Masiv.BetApi.Models;
using Masiv.BetApi.Utilty;

namespace Masiv.BetApi.Providers
{
   

    public class RouletteProvider : IRouletteProvider
    {
        private readonly BetManager<Bet> _betManager;
        private readonly RouletteManager<Roulette> _rouletteManager;

        public RouletteProvider(BetManager<Bet> betManager,
                                RouletteManager<Roulette> rouletteManager)
        {
            _betManager = betManager;
            _rouletteManager = rouletteManager;
        }

        public async Task<Response<Roulette>> CreateAsync(Roulette roulette)
        {
            if (roulette == null)
                throw new ArgumentNullException(nameof(roulette));
            return await _rouletteManager.CreateAsync(roulette);
        }
  

        public async Task<List<Roulette>> GetAllAsync()
        {
            return  await _rouletteManager.GetAllAsync();
        }
        public async Task<Response<Bet>> AddToBetAsync(Bet bet)
        {
            if (bet == null)
                throw new ArgumentNullException(nameof(bet));
            if (await ExistsNotRouletteAsync(new Roulette { RouletteId = bet.RouletteId }))
                return new Response<Bet> { Succeeded = false, Message = "La rouleta no se encuentra registrada" };
            if (await IsCloseOutAsync(new Roulette{ RouletteId =  bet.RouletteId}))
                return new Response<Bet> { Succeeded = false, Message = "La rouleta ya se encuentra cerrada, Intente con una ruleta abierta." };
            bet.BetType = bet.NumberBet <= 36 ? (int)BetType.Number : (int)BetType.Color;
            return await _betManager.CreateAsync(bet);
        }
        public async  Task<Response> OpenAsync(Roulette roulette)
        {
            if (roulette == null)
                throw new ArgumentNullException(nameof(roulette));
            if (await ExistsNotRouletteAsync(roulette))
                return new Response { Succeeded = false, Message = "La rouleta no se encuentra registrada" };
            Roulette rouletteFound = await _rouletteManager.FindByIdAsync(roulette);
            rouletteFound.IsOpen = true;
            rouletteFound.OpeningDate = Utility.GetFormatDateTimeIso8601();
            return await _rouletteManager.UpdateAsync(rouletteFound);
        }
        public async Task<Response<List<BetResult>>> CloseAsync(Roulette roulette)
        {
            if (roulette == null)
                throw new ArgumentNullException(nameof(roulette));
            if (await ExistsNotRouletteAsync(roulette))
                return new Response<List<BetResult>> { Succeeded = false, Message = "La rouleta no se encuentra registrada" };
            if (await IsCloseOutAsync(roulette))
                return new Response<List<BetResult>> { Succeeded = false, Message = "La rouleta ya se encuentra cerrada"};
            Task<List<Bet>> bets = _betManager.GetAllByRouletteIdAsync(new Bet {RouletteId = roulette.RouletteId});
            Task<Roulette> rouletteFound = _rouletteManager.FindByIdAsync(roulette);
            await Task.WhenAll(bets, rouletteFound);
            rouletteFound.Result.IsOpen = false;
            rouletteFound.Result.ClosingDate = Utility.GetFormatDateTimeIso8601();
            await _rouletteManager.UpdateAsync(rouletteFound.Result);
            return new Response<List<BetResult>>
           {
               Message = "Resultado de apuestas",
              Succeeded =  true,
              Result = this.GetBetResults(bets.Result)
           };
        }

        private List<BetResult> GetBetResults(List<Bet> bets)
        {
            List<BetResult> result = new List<BetResult>();
            int winNumber = GetWinningNumber();
            int numberWinColor = 0;
            if (winNumber % 2 == 0)
                numberWinColor = (int)Color.Rojo;
            else
                numberWinColor = (int)Color.Negro;
            var betsNumbers = bets.Where(x => x.BetType == (int)BetType.Number);
            var betsColor = bets.Where(x => x.BetType == (int)BetType.Color);
            foreach (var bet in betsNumbers)
            {
                result.Add(bet.NumberBet == winNumber
                    ? new BetResult { UserId = bet.UserId, EarnedMoney = bet.BetMoney * 5, IsWinner = true, BetMoney = bet.BetMoney, WinningNumber = winNumber , BetType = 1}
                    : new BetResult { UserId = bet.UserId, EarnedMoney = 0, IsWinner = false, BetMoney = bet.BetMoney,WinningNumber = winNumber, BetType = 1 });
            }
            foreach (var bet in betsColor)
            {
                result.Add(bet.NumberBet == numberWinColor
                    ? new BetResult { UserId = bet.UserId, EarnedMoney = (bet.BetMoney * 1.8M), IsWinner = true, BetMoney = bet.BetMoney, BetType = bet.BetType , WinningNumber = winNumber }
                    : new BetResult { UserId = bet.UserId, EarnedMoney = 0, IsWinner = false, BetMoney = bet.BetMoney, BetType = bet.BetType , WinningNumber = winNumber });
            }
            return result;
        }
        private int GetWinningNumber()
        {
            Random rnd = new Random();
            return rnd.Next(37);
        }

        public virtual async Task<bool> IsCloseOutAsync(Roulette roulette)
        {
            Roulette rouletteFound = await _rouletteManager.FindByIdAsync(roulette);
            if (rouletteFound.IsOpen == false)
                return true;
            else
                return false;
        }
        public virtual async Task<bool> ExistsNotRouletteAsync(Roulette roulette)
        {
            Roulette rouletteFound = await _rouletteManager.FindByIdAsync(roulette);
            return rouletteFound == null;
        }
        
    }
}
