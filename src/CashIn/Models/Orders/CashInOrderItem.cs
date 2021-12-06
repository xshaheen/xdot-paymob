// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Orders;

[PublicAPI]
public class CashInOrderItem {
    [JsonPropertyName("name")]
    public string Name { get; init; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; init; } = default!;

    [JsonPropertyName("amount_cents")]
    public int AmountCents { get; init; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }

    [JsonExtensionData]
    public IDictionary<string, object?>? ExtensionData { get; init; }
}
