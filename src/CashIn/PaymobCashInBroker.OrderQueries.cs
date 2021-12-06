// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net.Http.Json;
using Flurl;
using X.Paymob.CashIn.Models.Orders;

namespace X.Paymob.CashIn;

public partial class PaymobCashInBroker {
    public async Task<CashInOrdersPage?> GetOrdersPageAsync(CashInOrdersPageRequest? request = null) {
        string authToken = await _authenticator.GetAuthenticationTokenAsync();

        string requestUrl = Url.Combine(_config.ApiBaseUrl, "ecommerce/orders");

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

        HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);

        if (!response.IsSuccessStatusCode) {
            await PaymobRequestException.ThrowAsync(response);
        }

        return await response.Content.ReadFromJsonAsync<CashInOrdersPage>();
    }

    public async Task<CashInOrder?> GetOrderAsync(string orderId) {
        string authToken = await _authenticator.GetAuthenticationTokenAsync();
        string requestUrl = Url.Combine(_config.ApiBaseUrl, "ecommerce/orders", orderId);

        using var request = new HttpRequestMessage {
            Method = HttpMethod.Get,
            RequestUri = new Uri(requestUrl, UriKind.Absolute),
            Headers = {
                { "Authorization", $"Bearer {authToken}" },
            },
        };

        HttpResponseMessage response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode) {
            await PaymobRequestException.ThrowAsync(response);
        }

        return await response.Content.ReadFromJsonAsync<CashInOrder>();
    }
}
