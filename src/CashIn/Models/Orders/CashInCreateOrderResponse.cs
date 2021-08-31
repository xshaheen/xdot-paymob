// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;

namespace X.Paymob.CashIn.Models.Orders {
    [PublicAPI]
    public class CashInCreateOrderResponse {
        private readonly IReadOnlyList<object?>? _deliveryStatus;
        private readonly IReadOnlyList<CashInOrderItem>? _items;

        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("merchant_order_id")]
        public string? MerchantOrderId { get; init; }

        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset CreatedAt { get; init; }

        [JsonPropertyName("amount_cents")]
        public int AmountCents { get; init; }

        [JsonPropertyName("paid_amount_cents")]
        public int PaidAmountCents { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonPropertyName("commission_fees")]
        public int CommissionFees { get; init; }

        [JsonPropertyName("delivery_fees_cents")]
        public int DeliveryFeesCents { get; init; }

        [JsonPropertyName("delivery_vat_cents")]
        public int DeliveryVatCents { get; init; }

        [JsonPropertyName("is_payment_locked")]
        public bool IsPaymentLocked { get; init; }

        [JsonPropertyName("is_return")]
        public bool IsReturn { get; init; }

        [JsonPropertyName("is_cancel")]
        public bool IsCancel { get; init; }

        [JsonPropertyName("is_returned")]
        public bool IsReturned { get; init; }

        [JsonPropertyName("is_canceled")]
        public bool IsCanceled { get; init; }

        [JsonPropertyName("delivery_needed")]
        public bool DeliveryNeeded { get; init; }

        [JsonPropertyName("notify_user_with_email")]
        public bool NotifyUserWithEmail { get; init; }

        [JsonPropertyName("payment_method")]
        public string PaymentMethod { get; init; } = default!;

        [JsonPropertyName("api_source")]
        public string ApiSource { get; init; } = default!;

        [JsonPropertyName("token")]
        public string? Token { get; init; }

        [JsonPropertyName("order_url")]
        public string? OrderUrl { get; init; }

        [JsonPropertyName("url")]
        public string? Url { get; init; }

        [JsonPropertyName("merchant")]
        public CashInOrderMerchant Merchant { get; init; } = default!;

        [JsonPropertyName("shipping_data")]
        public CashInOrderShippingData? ShippingData { get; init; }

        [JsonPropertyName("shipping_details")]
        public CashInOrderShippingDetails? ShippingDetails { get; init; }

        [JsonPropertyName("items")]
        public IReadOnlyList<CashInOrderItem> Items {
            get => _items ?? Array.Empty<CashInOrderItem>();
            init => _items = value;
        }

        [JsonPropertyName("delivery_status")]
        public IReadOnlyList<object?> DeliveryStatus {
            get => _deliveryStatus ?? Array.Empty<object?>();
            init => _deliveryStatus = value;
        }

        [JsonPropertyName("wallet_notification")]
        public object? WalletNotification { get; init; }

        [JsonPropertyName("merchant_staff_tag")]
        public object? MerchantStaffTag { get; init; }

        [JsonPropertyName("pickup_data")]
        public object? PickupData { get; init; }

        [JsonPropertyName("collector")]
        public object? Collector { get; init; }

        [JsonPropertyName("data")]
        public object? Data { get; init; }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
