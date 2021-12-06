// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Callback; 

[PublicAPI]
public class TransactionProcessedCallbackResponseObj {
    [JsonPropertyName("encoding")]
    public string? Encoding { get; init; }

    [JsonPropertyName("headers")]
    public string? Headers { get; init; }

    [JsonPropertyName("content")]
    public string? Content { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }
}