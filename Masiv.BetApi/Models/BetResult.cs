using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Masiv.BetApi.Models
{
    public class BetResult
    {
        public  string UserId { get; set; }
        public bool IsWinner { get; set; }
         
        public  decimal EarnedMoney { get; set; }

        public  int WinningNumber { get; set; }

        public decimal BetMoney { get; set; }

        public  int BetType { get; set; }

    }
}
