using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masiv.BetApi.Models;
using Masiv.BetApi.Providers;
using Masiv.BetApi.Stores;
using Masiv.BetApi.Utilty;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Masiv.BetApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouletteController : ControllerBase
    {
        private readonly IRouletteProvider _rouletteProvider;
        public RouletteController(IRouletteProvider rouletteProvider)
        {
            _rouletteProvider = rouletteProvider;
        }

        [HttpPost("CreateRoulette")]
        public Task<Response<Roulette>> CreateRoulette()
        {
            return _rouletteProvider.CreateAsync(new Roulette { IsOpen = false });
        }

        [HttpPut("{RouletteId}/OpenRoulette")]
        public Task<Response> OpenRoulette([FromRoute(Name = "RouletteId")] string rouletteId)
        {
            if (string.IsNullOrEmpty(rouletteId))
                return Task.FromResult(new Response {Message = "RouletteId is requeried", Succeeded = false});
            return _rouletteProvider.OpenAsync(new Roulette { RouletteId = rouletteId });
        }
        [HttpPut("{RouletteId}/CloseRoulette")]
        public Task<Response<List<BetResult>>> CloseRoulette([FromRoute(Name = "RouletteId")] string rouletteId)
        {
            if (string.IsNullOrEmpty(rouletteId))
                return Task.FromResult(new Response<List<BetResult>> { Message = "rouletteId is requeried", Succeeded = false });
            return _rouletteProvider.CloseAsync(new Roulette { RouletteId = rouletteId });
        }
        /// <summary>
        /// Creates a Bet.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     Header :{userId : "c7acbb4f"}
        ///     POST /ToBet
        ///     {
        ///        "RouletteId": "c7acbb4f-f827-4bab-8cab-73b8cad30296",
        ///        "BetMoney": 12000,
        ///        "NumberBet": 10,
        ///     }
        ///
        /// </remarks>
        /// <param name="userId"></param>
        /// <param name="request">"NumberBet is less than or equal 36 =>  Bet is type Number or NumberBet greater than  36 Bet is type Color. rojo = 37 , black = 38"</param>
        /// <returns>A Response</returns>
        /// <response Succeeded="true">Returns the newly-created Bet</response>
        /// <response Succeeded="false">If the item is null or model state is invalid</response>    
        [HttpPost("ToBet")]
        public async Task<Response<Bet>> ToBet([FromHeader(Name = "userId")] string userId, [FromBody] BetRequest request)
        {
            if (string.IsNullOrEmpty(userId))
                return new Response<Bet> {Message = "userId is requeried", Succeeded = false};
           
            return await _rouletteProvider.AddToBetAsync(new Bet
            {
                UserId = userId,
                RouletteId = request.RouletteId,
                BetMoney = request.BetMoney,
                NumberBet = request.NumberBet
            });
        }
        [HttpGet("GetAllRoulettes")]
        public async Task<List<Roulette>> GetAllRoulettes()
        {

            return await _rouletteProvider.GetAllAsync();
        }
    }
}
