using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Amazon.SecretsManager;
using Masiv.BetApi.Extensions;
using Masiv.BetApi.Models;
using Masiv.BetApi.Providers;
using Masiv.BetApi.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;
using IWebHostEnvironment = Microsoft.AspNetCore.Hosting.IWebHostEnvironment;

namespace Masiv.BetApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration , IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        protected IWebHostEnvironment Environment;


        public void ConfigureServices(IServiceCollection services)
        {

            services.AddLogging(config =>
            {
                if (!Environment.IsDevelopment())
                {
                    config.AddAWSProvider(Configuration.GetAWSLoggingConfigSection());
                    config.SetMinimumLevel(LogLevel.Error);
                }
            });
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(new RedisConfiguration
            {
                ConnectionString = Configuration["ConnectionStrings:MyTestApp:mysecret"]
            });
            services.AddScoped<IBetStore<Bet>, BetStore>();
            services.AddScoped<IRouletteStore<Roulette>, RouletteStore>();
            services.AddScoped<IRouletteProvider, RouletteProvider>();
            services.TryAddScoped<BetManager<Bet>>();
            services.TryAddScoped<RouletteManager<Roulette>>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCustomExceptionMiddleware();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
