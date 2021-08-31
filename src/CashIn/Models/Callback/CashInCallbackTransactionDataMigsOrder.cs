// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;

namespace X.Paymob.CashIn.Models.Callback {
    [PublicAPI]
    public class CashInCallbackTransactionDataMigsOrder {
        [JsonPropertyName("acceptPartialAmount")]
        public bool AcceptPartialAmount { get; init; }

        [JsonPropertyName("id")]
        public string Id { get; init; } = default!;

        [JsonPropertyName("status")]
        public string Status { get; init; } = default!;

        [JsonPropertyName("totalAuthorizedAmount")]
        public decimal TotalAuthorizedAmount { get; init; }

        [JsonPropertyName("totalCapturedAmount")]
        public decimal TotalCapturedAmount { get; init; }

        [JsonPropertyName("totalRefundedAmount")]
        public decimal TotalRefundedAmount { get; init; }

        [JsonPropertyName("creationTime")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset CreationTime { get; init; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
