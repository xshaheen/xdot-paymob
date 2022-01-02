// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net.Http.Json;
using Flurl;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using X.Paymob.CashIn.Models;
using X.Paymob.CashIn.Models.Auth;

namespace X.Paymob.CashIn;

public class PaymobCashInAuthenticator : IPaymobCashInAuthenticator {
    private const string _CacheKey = "PaymobToken-09841B14-7E703AF44F43";
    private readonly HttpClient _httpClient;
    private readonly IMemoryCache _memoryCache;
    private readonly IOptionsMonitor<CashInConfig> _options;

    public PaymobCashInAuthenticator(
        HttpClient httpClient,
        IMemoryCache memoryCache,
        IOptionsMonitor<CashInConfig> options
    ) {
        _httpClient = httpClient;
        _memoryCache = memoryCache;
        _options = options;

        options.OnChange(_ => _memoryCache.Remove(_CacheKey));
    }

    public async Task<CashInAuthenticationTokenResponse> RequestAuthenticationTokenAsync() {
        var config = _options.CurrentValue;
        var requestUrl = Url.Combine(config.ApiBaseUrl, "auth/tokens");
        var request = new CashInAuthenticationTokenRequest { ApiKey = config.ApiKey };
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(requestUrl, request);

        if (!response.IsSuccessStatusCode) {
            await PaymobRequestException.ThrowAsync(response);
        }

        var content = await response.Content.ReadFromJsonAsync<CashInAuthenticationTokenResponse>();

        _memoryCache.Set(_CacheKey, content!.Token, new MemoryCacheEntryOptions {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(55),
        });

        return content;
    }

    public async ValueTask<string> GetAuthenticationTokenAsync() {
        if (_memoryCache.TryGetValue(_CacheKey, out string token)) {
            return token;
        }

        var response = await RequestAuthenticationTokenAsync();

        return response.Token;
    }
}
