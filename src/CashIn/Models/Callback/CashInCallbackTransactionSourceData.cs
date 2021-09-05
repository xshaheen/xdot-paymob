// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Callback {
    [PublicAPI]
    public class CashInCallbackTransactionSourceData {
        [JsonPropertyName("pan")]
        public string? Pan { get; init; }

        [JsonPropertyName("type")]
        public string? Type { get; init; }

        [JsonPropertyName("sub_type")]
        public string? SubType { get; init; }

        [JsonPropertyName("tenure")]
        public object? Tenure { get; init; }

        /// <summary>Only if Wallet or Accept Kiosk.</summary>
        [JsonPropertyName("phone_number")]
        public string? PhoneNumber { get; init; }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
