using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv.BetApi.Models
{

    public class Bet 
    {

        public string BetId { get; set; }

        public string UserId { get; set; }

        public string RouletteId { get; set; }
   
        public int NumberBet { get; set; }
        public int BetType { get; set; }

        public decimal BetMoney { get; set; }
        /// <summary>
        /// Initializes a new instance of <see cref="Bet"/>.
        /// </summary>
        /// <remarks>
        /// The Id property is initialized to form a new GUID string value.
        /// </remarks>
        /// 
        public Bet()
        {
            BetId = Guid.NewGuid().ToString();
  
        }

    }
}
