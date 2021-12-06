// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net.Http.Json;
using Flurl;
using X.Paymob.CashIn.Models.Payment;

namespace X.Paymob.CashIn;

public partial class PaymobCashInBroker {
    /// <summary>
    /// Get payment key which is used to authenticate payment request and verifying transaction
    /// request metadata.
    /// </summary>
    public async Task<CashInPaymentKeyResponse> RequestPaymentKeyAsync(CashInPaymentKeyRequest request) {
        string authToken = await _authenticator.GetAuthenticationTokenAsync();
        var requestUrl = Url.Combine(_config.ApiBaseUrl, "acceptance/payment_keys");
        var internalRequest = new CashInPaymentKeyInternalRequest(request, authToken, _config.ExpirationPeriod);
        HttpResponseMessage response = await _httpClient.PostAsJsonAsync(requestUrl, internalRequest);

        if (!response.IsSuccessStatusCode)
            await PaymobRequestException.ThrowFor(response);

        return (await response.Content.ReadFromJsonAsync<CashInPaymentKeyResponse>())!;
    }
}
