// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;

namespace X.Paymob.CashIn.Models.Callback {
    [PublicAPI]
    public class TransactionProcessedCallbackResponse {
        [JsonPropertyName("response_received_at")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset ResponseReceivedAt { get; init; }

        [JsonPropertyName("callback_url")]
        public string CallbackUrl { get; init; } = default!;

        [JsonPropertyName("response")]
        public TransactionProcessedCallbackResponseObj Response { get; init; } = default!;
    }
}
