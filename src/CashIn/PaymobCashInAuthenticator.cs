// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using X.Paymob.CashIn.Models;
using X.Paymob.CashIn.Models.Auth;

namespace X.Paymob.CashIn {
    public class PaymobCashInAuthenticator : IPaymobCashInAuthenticator {
        private static readonly long _MaxTicks = TimeSpan.FromMinutes(59).Ticks;
        private readonly IClockBroker _clockBroker;
        private readonly HttpClient _httpClient;
        private readonly IOptionsMonitor<CashInConfig> _options;
        private long? _createdAtTicks;
        private string? _token;

        public PaymobCashInAuthenticator(
            HttpClient httpClient,
            IClockBroker clockBroker,
            IOptionsMonitor<CashInConfig> options
        ) {
            _httpClient = httpClient;
            _clockBroker = clockBroker;
            _options = options;
            options.OnChange(_ => _InvalidateCache());
        }

        public async Task<CashInAuthenticationTokenResponse> RequestAuthenticationTokenAsync() {
            string apiKey = _options.CurrentValue.ApiKey;
            var request = new CashInAuthenticationTokenRequest { ApiKey = apiKey };
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("auth/tokens", request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadFromJsonAsync<CashInAuthenticationTokenResponse>();
            _Cache(content!.Token);
            return content;
        }

        public async ValueTask<string> GetAuthenticationTokenAsync() {
            if (_token is not null && _clockBroker.TicksNow - _createdAtTicks < _MaxTicks) {
                return _token;
            }

            var response = await RequestAuthenticationTokenAsync();
            return response.Token;
        }

        private void _Cache(string token) {
            _token = token;
            _createdAtTicks = _clockBroker.TicksNow;
        }

        private void _InvalidateCache() {
            _token = null;
            _createdAtTicks = null;
        }
    }
}
