// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Callback; 

[PublicAPI]
public class CashInCallbackTransactionDataMigsTransaction {
    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("frequency")]
    public string Frequency { get; init; } = default!;

    [JsonPropertyName("authorizationCode")]
    public string? AuthorizationCode { get; init; }

    [JsonPropertyName("type")]
    public string? Type { get; init; }

    [JsonPropertyName("receipt")]
    public string? Receipt { get; init; }

    [JsonPropertyName("terminal")]
    public string? Terminal { get; init; }

    [JsonPropertyName("source")]
    public string? Source { get; init; }

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    [JsonPropertyName("currency")]
    public string Currency { get; init; } = default!;

    [JsonPropertyName("acquirer")]
    public CashInCallbackTransactionDataAcquirer Acquirer { get; init; } = default!;

    [JsonExtensionData]
    public IDictionary<string, object?>? ExtensionData { get; init; }
}