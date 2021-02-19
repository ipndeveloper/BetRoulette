using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv.BetApi.Models
{
    public class Roulette
    {
        public  string RouletteId { get; set; }
        public  bool IsOpen { get; set; }
        public  DateTime OpeningDate { get; set; }
        public DateTime  ClosingDate { get; set; }
        public Roulette()
        {
            RouletteId =  Guid.NewGuid().ToString();
        }

        public static implicit operator Roulette(Response<Roulette> v)
        {
            throw new NotImplementedException();
        }
    }
}
