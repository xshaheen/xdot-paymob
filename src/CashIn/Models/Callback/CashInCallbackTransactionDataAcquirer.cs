// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Callback; 

[PublicAPI]
public class CashInCallbackTransactionDataAcquirer {
    [JsonPropertyName("settlementDate")]
    public string SettlementDate { get; init; } = default!;

    [JsonPropertyName("timeZone")]
    public string TimeZone { get; init; } = default!;

    [JsonPropertyName("id")]
    public string Id { get; init; } = default!;

    [JsonPropertyName("date")]
    public string Date { get; init; } = default!;

    [JsonPropertyName("merchantId")]
    public string MerchantId { get; init; } = default!;

    [JsonPropertyName("transactionId")]
    public string TransactionId { get; init; } = default!;

    [JsonPropertyName("batch")]
    public int Batch { get; init; }
}