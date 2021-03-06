﻿using System;
using System.Collections.Concurrent;
using Ketchup.Core.Address;
using NConsul;

namespace Ketchup.Consul.Internal.ClientProvider.Implementation
{
    public class ConsulClientProvider : IConsulClientProvider
    {
        private readonly ConcurrentDictionary<string, ConsulClient> _consulClient = new ConcurrentDictionary<string, ConsulClient>();

        public Configurations.AppConfig AppConfig { get; set; }

        public ConsulClient GetConsulClient()
        {
            ConsulClient result = null;

            result = _consulClient.GetOrAdd($"{AppConfig.Addresse.Ip}:{AppConfig.Addresse.Port}", new ConsulClient(
                config => { config.Address = new Uri($"http://{AppConfig.Addresse.Ip}:{AppConfig.Addresse?.Port}"); },
                null, h =>
                {
                    h.UseProxy = false;
                    h.Proxy = null;
                }));

            return result;
        }
    }
}