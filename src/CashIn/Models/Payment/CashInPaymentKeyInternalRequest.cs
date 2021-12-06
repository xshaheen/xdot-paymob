// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;

namespace X.Paymob.CashIn.Models.Payment; 

internal class CashInPaymentKeyInternalRequest {
    public CashInPaymentKeyInternalRequest(
        CashInPaymentKeyRequest request,
        string authToken,
        int defaultExpiration
    ) {
        AuthToken = authToken;
        OrderId = request.OrderId;
        IntegrationId = request.IntegrationId;
        AmountCents = request.AmountCents.ToString(CultureInfo.InvariantCulture);
        Expiration = request.Expiration ?? defaultExpiration;
        Currency = request.Currency;
        LockOrderWhenPaid = request.LockOrderWhenPaid ? "true" : "false";
        BillingData = request.BillingData;
    }

    [JsonPropertyName("auth_token")]
    public string AuthToken { get; }

    [JsonPropertyName("integration_id")]
    public int IntegrationId { get; }

    [JsonPropertyName("order_id")]
    public int OrderId { get; }

    [JsonPropertyName("amount_cents")]
    public string AmountCents { get; }

    [JsonPropertyName("expiration")]
    public int Expiration { get; }

    [JsonPropertyName("currency")]
    public string Currency { get; }

    [JsonPropertyName("lock_order_when_paid")]
    public string LockOrderWhenPaid { get; }

    [JsonPropertyName("billing_data")]
    public CashInBillingData BillingData { get; }
}