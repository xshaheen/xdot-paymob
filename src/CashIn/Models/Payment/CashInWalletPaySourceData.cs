// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Payment {
    [PublicAPI]
    public class CashInWalletPaySourceData {
        [JsonPropertyName("owner_name")]
        public string? OwnerName { get; init; }

        [JsonPropertyName("sub_type")]
        public string SubType { get; init; } = default!;

        [JsonPropertyName("pan")]
        public string Pan { get; init; } = default!;

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; init; } = default!;

        [JsonPropertyName("type")]
        public string Type { get; init; } = default!;

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
