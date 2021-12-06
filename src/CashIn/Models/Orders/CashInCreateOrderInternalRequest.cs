// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Orders;

[PublicAPI]
internal class CashInCreateOrderInternalRequest {
    public CashInCreateOrderInternalRequest(string authToken, CashInCreateOrderRequest request) {
        AuthToken = authToken;
        AmountCents = request.AmountCents;
        Currency = request.Currency;
        MerchantOrderId = request.MerchantOrderId;
        DeliveryNeeded = request.DeliveryNeeded;
        ShippingData = request.ShippingData;
        ShippingDetails = request.ShippingDetails;
        Items = request.Items;
    }

    [JsonPropertyName("auth_token")]
    public string AuthToken { get; }

    [JsonPropertyName("amount_cents")]
    public int AmountCents { get; }

    [JsonPropertyName("currency")]
    public string Currency { get; }

    [JsonPropertyName("merchant_order_id")]
    public string? MerchantOrderId { get; }

    /// <summary>
    /// Set it to be true if your order needs to be delivered by Accept's product delivery services.
    /// </summary>
    [JsonPropertyName("delivery_needed")]
    public string DeliveryNeeded { get; }

    /// <summary>Mandatory if your order needs to be delivered, otherwise you can delete the whole object.</summary>
    [JsonPropertyName("shipping_data")]
    public CashInCreateOrderRequestShippingData? ShippingData { get; }

    /// <summary>Mandatory if your order needs to be delivered, otherwise you can delete the whole object.</summary>
    [JsonPropertyName("shipping_details")]
    public CashInCreateOrderRequestShippingDetails? ShippingDetails { get; }

    /// <summary>The list of objects contains the contents of the order if it is existing.</summary>
    [JsonPropertyName("items")]
    public IEnumerable<CashInCreateOrderRequestOrderItem> Items { get; }
}
