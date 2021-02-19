using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv.BetApi.Models
{
    public class BetRequest
    {
        [Required()]
        public string RouletteId { get; set; }
        [Required()]
        [Range(0, 38, ErrorMessage = "Number must be between 0 and 38")]
        public int NumberBet { get; set; }
        public int BetType { get; set; }
        [Required()]
        [Range(1, 10000.0, ErrorMessage = "Money must be between 1 and 10000")]
        public decimal BetMoney { get; set; }
    }
}
