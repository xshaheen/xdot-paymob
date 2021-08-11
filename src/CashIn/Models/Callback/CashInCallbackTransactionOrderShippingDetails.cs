// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Callback {
    [PublicAPI]
    public class CashInCallbackTransactionOrderShippingDetails {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("cash_on_delivery_amount")]
        public int CashOnDeliveryAmount { get; init; }

        [JsonPropertyName("cash_on_delivery_type")]
        public string? CashOnDeliveryType { get; init; }

        [JsonPropertyName("is_same_day")]
        public int IsSameDay { get; init; }

        [JsonPropertyName("number_of_packages")]
        public int NumberOfPackages { get; init; }

        [JsonPropertyName("weight")]
        public int Weight { get; init; }

        [JsonPropertyName("weight_unit")]
        public string WeightUnit { get; init; } = default!;

        [JsonPropertyName("length")]
        public int Length { get; init; }

        [JsonPropertyName("width")]
        public int Width { get; init; }

        [JsonPropertyName("height")]
        public int Height { get; init; }

        [JsonPropertyName("delivery_type")]
        public string? DeliveryType { get; init; }

        [JsonPropertyName("order_id")]
        public int OrderId { get; init; }

        [JsonPropertyName("order")]
        public int Order { get; init; }

        [JsonPropertyName("notes")]
        public string? Notes { get; init; }

        [JsonPropertyName("latitude")]
        public JsonElement Latitude { get; init; }

        [JsonPropertyName("longitude")]
        public JsonElement Longitude { get; init; }

        [JsonPropertyName("return_type")]
        public JsonElement ReturnType { get; init; }

        [JsonExtensionData]
        public IDictionary<string, JsonElement>? ExtensionData { get; init; }
    }
}
