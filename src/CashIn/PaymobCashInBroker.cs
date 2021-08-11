// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net.Http;
using Microsoft.Extensions.Options;
using X.Paymob.CashIn.Models;

namespace X.Paymob.CashIn {
    public partial class PaymobCashInBroker : IPaymobCashInBroker {
        private readonly IPaymobCashInAuthenticator _authenticator;
        private readonly CashInConfig _config;
        private readonly HttpClient _httpClient;

        public PaymobCashInBroker(
            HttpClient httpClient,
            IPaymobCashInAuthenticator authenticator,
            IOptionsMonitor<CashInConfig> payInOptions
        ) {
            _httpClient = httpClient;
            _authenticator = authenticator;
            _config = payInOptions.CurrentValue;
        }
    }
}
