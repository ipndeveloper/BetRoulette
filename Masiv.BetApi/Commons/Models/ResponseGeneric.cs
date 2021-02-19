using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv.BetApi.Models
{
    public class Response<T> : Response where  T : class
    {
        public  T Result { get; set; }
    }
}
