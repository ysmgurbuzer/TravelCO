using Application.Travel.Features.CQRS.Handlers.HousingHandlers;
using Application.Travel.Interfaces;
using Application.Travel.Services;
using Application.Travel.Tools;
using Braintree;
using Domain.Travel.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Travel
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<RedisService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof
                (DependencyExtension).Assembly));
            

            services.AddSingleton<IBraintreeGateway>(provider =>
            {
                var configuration = provider.GetRequiredService<IConfiguration>();
                var environment = configuration["Braintree:Environment"];
                var merchantId = configuration["Braintree:MerchantId"];
                var publicKey = configuration["Braintree:PublicKey"];
                var privateKey = configuration["Braintree:PrivateKey"];

                return new BraintreeGateway
                {
                    Environment = environment.Equals("Production") ? Braintree.Environment.PRODUCTION : Braintree.Environment.SANDBOX,
                    MerchantId = merchantId,
                    PublicKey = publicKey,
                    PrivateKey = privateKey
                };
            });

        }

    }
}
