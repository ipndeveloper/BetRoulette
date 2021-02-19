using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Masiv.BetApi.Customs.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Masiv.BetApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
