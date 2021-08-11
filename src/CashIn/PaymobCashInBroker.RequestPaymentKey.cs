// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using X.Paymob.CashIn.Models.Payment;

namespace X.Paymob.CashIn {
    public partial class PaymobCashInBroker {
        /// <summary>
        /// Get payment key which is used to authenticate payment request and verifying transaction
        /// request metadata.
        /// </summary>
        public async Task<CashInPaymentKeyResponse> RequestPaymentKeyAsync(CashInPaymentKeyRequest request) {
            string authToken = await _authenticator.GetAuthenticationTokenAsync();
            var req = new CashInPaymentKeyInternalRequest(authToken, request);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("acceptance/payment_keys", req);
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<CashInPaymentKeyResponse>())!;
        }
    }
}
