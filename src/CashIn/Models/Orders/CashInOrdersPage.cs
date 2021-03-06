// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Orders;

[PublicAPI]
public class CashInOrdersPage {
    private readonly IReadOnlyCollection<CashInOrder>? _results;

    [JsonPropertyName("count")]
    public int Count { get; init; }

    [JsonPropertyName("next")]
    public string? Next { get; init; }

    [JsonPropertyName("previous")]
    public string? Previous { get; init; }

    [JsonPropertyName("results")]
    public IReadOnlyCollection<CashInOrder> Results {
        get => _results ?? Array.Empty<CashInOrder>();
        init => _results = value;
    }

    [JsonExtensionData]
    public IDictionary<string, object?>? ExtensionData { get; init; }

    public bool HasPrevious() {
        return Previous is not null;
    }

    public bool HasNext() {
        return Next is not null;
    }
}
