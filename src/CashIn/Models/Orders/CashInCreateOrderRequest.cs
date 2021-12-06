// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using Ardalis.GuardClauses;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Orders; 

[PublicAPI]
public class CashInCreateOrderRequest {
    private CashInCreateOrderRequest() { }

    public int AmountCents { get; private init; }

    public string Currency { get; private init; } = default!;

    public string? MerchantOrderId { get; private init; }

    /// <summary>
    /// Set it to be true if your order needs to be delivered by Accept's product delivery services.
    /// </summary>
    public string DeliveryNeeded { get; private init; } = "false";

    /// <summary>
    /// Mandtaory if your order needs to be delivered, otherwise you can delete the whole object.
    /// </summary>
    public CashInCreateOrderRequestShippingData? ShippingData { get; private init; }

    /// <summary>
    /// Mandatory if your order needs to be delivered, otherwise you can delete the whole object.
    /// </summary>
    public CashInCreateOrderRequestShippingDetails? ShippingDetails { get; private init; }

    /// <summary>
    /// The list of objects contains the contents of the order if it is existing.
    /// </summary>
    public IEnumerable<CashInCreateOrderRequestOrderItem> Items { get; private init; } =
        Array.Empty<CashInCreateOrderRequestOrderItem>();

    /// <summary>Create order without delivery.</summary>
    public static CashInCreateOrderRequest CreateOrder(
        int amountCents,
        string currency = "EGP",
        string? merchantOrderId = null
    ) {
        Guard.Against.NegativeOrZero(amountCents, nameof(amountCents));
        Guard.Against.NullOrEmpty(currency, nameof(currency));

        return new() {
            AmountCents = amountCents,
            DeliveryNeeded = "false",
            Currency = currency,
            MerchantOrderId = merchantOrderId,
        };
    }

    /// <summary>Create delivery order.</summary>
    public static CashInCreateOrderRequest CreateDeliveryOrder(
        CashInCreateOrderRequestShippingDetails shippingDetails,
        CashInCreateOrderRequestShippingData shippingData,
        ICollection<CashInCreateOrderRequestOrderItem> items,
        int amountCents,
        string currency = "EGP",
        string? merchantOrderId = null
    ) {
        Guard.Against.NegativeOrZero(amountCents, nameof(amountCents));
        Guard.Against.NullOrEmpty(currency, nameof(currency));
        Guard.Against.Null(shippingDetails, nameof(shippingDetails));
        Guard.Against.Null(shippingData, nameof(shippingData));
        Guard.Against.NullOrEmpty(items, nameof(items));

        return new() {
            AmountCents = amountCents,
            DeliveryNeeded = "true",
            Currency = currency,
            MerchantOrderId = merchantOrderId,
            ShippingDetails = shippingDetails,
            ShippingData = shippingData,
            Items = items,
        };
    }
}