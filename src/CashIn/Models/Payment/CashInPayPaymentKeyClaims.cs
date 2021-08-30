// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Payment {
    [PublicAPI]
    public class CashInPayPaymentKeyClaims {
        [JsonPropertyName("integration_id")]
        public int IntegrationId { get; init; }

        [JsonPropertyName("amount_cents")]
        public int AmountCents { get; init; }

        [JsonPropertyName("user_id")]
        public int UserId { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonPropertyName("exp")]
        public int Exp { get; init; }

        [JsonPropertyName("order_id")]
        public int OrderId { get; init; }

        [JsonPropertyName("pmk_ip")]
        public string PmkIp { get; init; } = default!;

        [JsonPropertyName("lock_order_when_paid")]
        public bool LockOrderWhenPaid { get; init; }

        [JsonPropertyName("billing_data")]
        public CashInPayPaymentKeyClaimsBillingData BillingData { get; init; } = default!;

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
