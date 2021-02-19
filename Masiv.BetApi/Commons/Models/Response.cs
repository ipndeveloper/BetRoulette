using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masiv.BetApi.Models
{
    public class Response
    {
        private static readonly Response _success = new Response { Succeeded = true };
        public bool Succeeded { get;  set; }
        public string Message { get; set; }
        public string[] Errors { get; set; }
        public static Response Success => _success;
    }
}
