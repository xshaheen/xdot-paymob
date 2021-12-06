// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Orders; 

[PublicAPI]
public class CashInCreateOrderRequestShippingDetails {
    [JsonPropertyName("number_of_packages")]
    public int NumberOfPackages { get; init; }

    [JsonPropertyName("weight")]
    public int Weight { get; init; }

    [JsonPropertyName("length")]
    public int Length { get; init; }

    [JsonPropertyName("width")]
    public int Width { get; init; }

    [JsonPropertyName("height")]
    public int Height { get; init; }

    [JsonPropertyName("weight_unit")]
    public string WeightUnit { get; init; } = "Kilogram";

    [JsonPropertyName("contents")]
    public string Contents { get; init; } = default!;

    [JsonPropertyName("notes")]
    public string Notes { get; init; } = default!;
}