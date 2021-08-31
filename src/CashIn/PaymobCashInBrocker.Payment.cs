// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Flurl;
using X.Paymob.CashIn.Models.Payment;

namespace X.Paymob.CashIn {
    public partial class PaymobCashInBroker {
        public string CreateIframeSrc(string iframeId, string token) {
            return Url
                .Combine(_config.IframeBaseUrl, iframeId)
                .SetQueryParams(new { payment_token = token });
        }

        public async Task<CashInWalletPayResponse> CreateWalletPayAsync(string paymentKey, string phoneNumber) {
            Guard.Against.NullOrEmpty(paymentKey, nameof(paymentKey));
            Guard.Against.NullOrEmpty(phoneNumber, nameof(phoneNumber));

            var request = new CashInPayRequest {
                Source = CashInSource.Wallet(phoneNumber),
                PaymentToken = paymentKey,
            };

            return await _PayAsync<CashInWalletPayResponse>(request);
        }

        public async Task<CashInKioskPayResponse> CreateKioskPayAsync(string paymentKey) {
            Guard.Against.NullOrEmpty(paymentKey, nameof(paymentKey));

            var request = new CashInPayRequest {
                Source = CashInSource.Kiosk,
                PaymentToken = paymentKey,
            };

            return await _PayAsync<CashInKioskPayResponse>(request);
        }

        public async Task<CashInCashCollectionPayResponse> CreateCashCollectionPayAsync(string paymentKey) {
            Guard.Against.NullOrEmpty(paymentKey, nameof(paymentKey));

            var request = new CashInPayRequest {
                Source = CashInSource.Cash,
                PaymentToken = paymentKey,
            };

            return await _PayAsync<CashInCashCollectionPayResponse>(request);
        }

        public async Task<CashInSavedTokenPayResponse> CreateSavedTokenPayAsync(string paymentKey, string savedToken) {
            Guard.Against.NullOrEmpty(paymentKey, nameof(paymentKey));
            Guard.Against.NullOrEmpty(savedToken, nameof(savedToken));

            var request = new CashInPayRequest {
                Source = CashInSource.SavedToken(savedToken),
                PaymentToken = paymentKey,
            };

            return await _PayAsync<CashInSavedTokenPayResponse>(request);
        }

        private async Task<TResponse> _PayAsync<TResponse>(CashInPayRequest request) {
            var requestUrl = Url.Combine(_config.ApiBaseUrl, "acceptance/payments/pay");
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(requestUrl, request);
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<TResponse>())!;
        }
    }
}
