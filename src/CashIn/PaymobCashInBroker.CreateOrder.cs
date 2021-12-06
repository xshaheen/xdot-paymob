// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Flurl;
using X.Paymob.CashIn.Models.Orders;

namespace X.Paymob.CashIn;

public partial class PaymobCashInBroker {
    private static readonly JsonSerializerOptions _IgnoreNullOptions = new(JsonSerializerDefaults.Web) {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    /// <summary>Create order. Order is a logical container for a transaction(s).</summary>
    public async Task<CashInCreateOrderResponse> CreateOrderAsync(CashInCreateOrderRequest request) {
        string authToken = await _authenticator.GetAuthenticationTokenAsync();
        var requestUrl = Url.Combine(_config.ApiBaseUrl, "ecommerce/orders");
        var internalRequest = new CashInCreateOrderInternalRequest(authToken, request);
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(requestUrl, internalRequest, _IgnoreNullOptions);

        if (!response.IsSuccessStatusCode)
            await PaymobRequestException.ThrowFor(response);

        return (await response.Content.ReadFromJsonAsync<CashInCreateOrderResponse>())!;
    }
}
