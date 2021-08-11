// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Flurl;
using X.Paymob.CashIn.Models.Transactions;

namespace X.Paymob.CashIn {
    public partial class PaymobCashInBroker {
        public async Task<CashInTransactionsPage?> GetTransactionsPageAsync(CashInTransactionsPageRequest request) {
            string authToken = await _authenticator.GetAuthenticationTokenAsync();
            string requestUrl = "acceptance/transactions".SetQueryParams(request.Query);

            using var requestMessage = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUrl, UriKind.Relative),
                Headers = {
                    { "Authorization", $"Bearer {authToken}" },
                },
            };

            HttpResponseMessage response = await _httpClient.SendAsync(requestMessage);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CashInTransactionsPage>();
        }

        public async Task<CashInTransaction?> GetTransactionAsync(string transactionId) {
            string authToken = await _authenticator.GetAuthenticationTokenAsync();
            string requestUrl = Url.Combine("acceptance/transactions", transactionId);

            using var request = new HttpRequestMessage {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUrl, UriKind.Relative),
                Headers = {
                    { "Authorization", $"Bearer {authToken}" },
                },
            };

            HttpResponseMessage response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CashInTransaction>();
        }
    }
}
