// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;
using X.Paymob.CashIn.Models.Orders;

namespace X.Paymob.CashIn.Models.Transactions {
    [PublicAPI]
    public class CashInTransaction {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("success")]
        public bool Success { get; init; }

        [JsonPropertyName("pending")]
        public bool Pending { get; init; }

        [JsonPropertyName("error_occured")]
        public bool ErrorOccured { get; init; }

        [JsonPropertyName("amount_cents")]
        public int AmountCents { get; init; }

        /// <summary>"N/A" or decimal number</summary>
        [JsonPropertyName("fees")]
        public string Fees { get; init; } = "0.0";

        /// <summary>"N/A" or decimal number</summary>
        [JsonPropertyName("vat")]
        public string Vat { get; init; } = "0.0";

        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset CreatedAt { get; init; }

        [JsonPropertyName("paid_at")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset? PaidAt { get; init; }

        [JsonPropertyName("is_auth")]
        public bool IsAuth { get; init; }

        [JsonPropertyName("is_capture")]
        public bool IsCapture { get; init; }

        [JsonPropertyName("refunded_amount_cents")]
        public int RefundedAmountCents { get; init; }

        [JsonPropertyName("source_id")]
        public int SourceId { get; init; }

        [JsonPropertyName("is_captured")]
        public bool IsCaptured { get; init; }

        [JsonPropertyName("captured_amount")]
        public int CapturedAmount { get; init; }

        [JsonPropertyName("owner")]
        public int Owner { get; init; }

        [JsonPropertyName("is_standalone_payment")]
        public bool IsStandalonePayment { get; init; }

        [JsonPropertyName("is_voided")]
        public bool IsVoided { get; init; }

        [JsonPropertyName("is_refunded")]
        public bool IsRefunded { get; init; }

        [JsonPropertyName("is_3d_secure")]
        public bool Is3dSecure { get; init; }

        [JsonPropertyName("is_void")]
        public bool IsVoid { get; init; }

        [JsonPropertyName("is_refund")]
        public bool IsRefund { get; init; }

        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; init; }

        [JsonPropertyName("is_live")]
        public bool IsLive { get; init; }

        [JsonPropertyName("integration_id")]
        public int IntegrationId { get; init; }

        [JsonPropertyName("profile_id")]
        public int ProfileId { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonPropertyName("api_source")]
        public string ApiSource { get; init; } = default!;

        [JsonPropertyName("has_parent_transaction")]
        public bool HasParentTransaction { get; init; }

        [JsonPropertyName("terminal_id")]
        public string? TerminalId { get; init; }

        [JsonPropertyName("is_cashout")]
        public bool IsCashOut { get; init; }

        [JsonPropertyName("is_upg")]
        public bool IsUpg { get; init; }

        [JsonPropertyName("integration_type")]
        public string? IntegrationType { get; init; }

        [JsonPropertyName("card_type")]
        public string? CardType { get; init; }

        [JsonPropertyName("routing_bank")]
        public string? RoutingBank { get; init; }

        [JsonPropertyName("card_holder_bank")]
        public string? CardHolderBank { get; init; }

        [JsonPropertyName("converted_gross_amount")]
        public object? ConvertedGrossAmount { get; init; }

        [JsonPropertyName("trx_settlement_curr")]
        public object? TrxSettlementCurr { get; init; }

        [JsonPropertyName("parent_transaction")]
        public object? ParentTransaction { get; init; }

        [JsonPropertyName("wallet_transaction_type")]
        public object? WalletTransactionType { get; init; }

        [JsonPropertyName("installment")]
        public object? Installment { get; init; }

        [JsonPropertyName("merchant_staff_tag")]
        public object? MerchantStaffTag { get; init; }

        [JsonPropertyName("other_endpoint_reference")]
        public object? OtherEndpointReference { get; init; }

        [JsonPropertyName("source_data")]
        public CashInTransactionSourceData? CashInTransactionSourceData { get; init; }

        [JsonPropertyName("data")]
        public CashInTransactionData? Data { get; init; }

        [JsonPropertyName("order")]
        public CashInOrder? Order { get; init; }

        [JsonPropertyName("billing_data")]
        public CashInTransactionBillingData? BillingData { get; init; }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; }
    }
}
