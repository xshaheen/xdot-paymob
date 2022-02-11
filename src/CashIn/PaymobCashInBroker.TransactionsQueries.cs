// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net.Http.Json;
using Flurl;
using X.Paymob.CashIn.Models.Transactions;

namespace X.Paymob.CashIn;

public partial class PaymobCashInBroker {
    public async Task<CashInTransactionsPage?> GetTransactionsPageAsync(CashInTransactionsPageRequest? request = null) {
        string authToken = await _authenticator.GetAuthenticationTokenAsync();

        string requestUrl = Url.Combine(_config.ApiBaseUrl, "acceptance/transactions");

        if (request is not null) {
            requestUrl = requestUrl.SetQueryParams(request.Query);
        }

        using var requestMessage = new HttpRequestMessage {
            Method = HttpMethod.Get,
            RequestUri = new Uri(requestUrl, UriKind.Absolute),
            Headers = {
                { "Authorization", $"Bearer {authToken}" },
            },
        };

        using var response = await _httpClient.SendAsync(requestMessage);

        if (!response.IsSuccessStatusCode) {
            await PaymobRequestException.ThrowAsync(response);
        }

        return await response.Content.ReadFromJsonAsync<CashInTransactionsPage>();
    }

    public async Task<CashInTransaction?> GetTransactionAsync(string transactionId) {
        string authToken = await _authenticator.GetAuthenticationTokenAsync();
        string requestUrl = Url.Combine(_config.ApiBaseUrl, $"acceptance/transactions/{transactionId}");

        using var request = new HttpRequestMessage {
            Method = HttpMethod.Get,
            RequestUri = new Uri(requestUrl, UriKind.Absolute),
            Headers = {
                { "Authorization", $"Bearer {authToken}" },
            },
        };

        using var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode) {
            await PaymobRequestException.ThrowAsync(response);
        }

        return await response.Content.ReadFromJsonAsync<CashInTransaction>();
    }
}
