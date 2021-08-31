// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;
using X.Paymob.CashIn.Models.Orders;

namespace X.Paymob.CashIn.Models.Payment {
    [PublicAPI]
    public class CashInWalletPayResponse {
        private readonly IReadOnlyList<object?>? _transactionProcessedCallbackResponses;

        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("pending")]
        public bool Pending { get; init; }

        [JsonPropertyName("amount_cents")]
        public int AmountCents { get; init; }

        [JsonPropertyName("success")]
        public bool Success { get; init; }

        [JsonPropertyName("is_auth")]
        public bool IsAuth { get; init; }

        [JsonPropertyName("is_capture")]
        public bool IsCapture { get; init; }

        [JsonPropertyName("is_standalone_payment")]
        public bool IsStandalonePayment { get; init; }

        [JsonPropertyName("is_voided")]
        public bool IsVoided { get; init; }

        [JsonPropertyName("is_refunded")]
        public bool IsRefunded { get; init; }

        [JsonPropertyName("is_3d_secure")]
        public bool Is3dSecure { get; init; }

        [JsonPropertyName("integration_id")]
        public int IntegrationId { get; init; }

        [JsonPropertyName("profile_id")]
        public int ProfileId { get; init; }

        [JsonPropertyName("has_parent_transaction")]
        public bool HasParentTransaction { get; init; }

        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset CreatedAt { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonPropertyName("api_source")]
        public string ApiSource { get; init; } = default!;

        [JsonPropertyName("merchant_commission")]
        public int MerchantCommission { get; init; }

        [JsonPropertyName("is_void")]
        public bool IsVoid { get; init; }

        [JsonPropertyName("is_refund")]
        public bool IsRefund { get; init; }

        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; init; }

        [JsonPropertyName("is_live")]
        public bool IsLive { get; init; }

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

        [JsonPropertyName("error_occured")]
        public bool ErrorOccured { get; init; }

        [JsonPropertyName("other_endpoint_reference")]
        public string OtherEndpointReference { get; init; } = default!;

        [JsonPropertyName("terminal_id")]
        public string? TerminalId { get; init; }

        [JsonPropertyName("redirect_url")]
        public string? RedirectUrl { get; init; }

        [JsonPropertyName("iframe_redirection_url")]
        public string? IframeRedirectionUrl { get; init; }

        [JsonPropertyName("data")]
        public CashInWalletData Data { get; init; } = default!;

        [JsonPropertyName("source_data")]
        public CashInWalletPaySourceData PaySourceData { get; init; } = default!;

        [JsonPropertyName("order")]
        public CashInOrder Order { get; init; } = default!;

        [JsonPropertyName("payment_key_claims")]
        public CashInPayPaymentKeyClaims PaymentKeyClaims { get; init; } = default!;

        [JsonPropertyName("merchant_staff_tag")]
        public object? MerchantStaffTag { get; init; }

        [JsonPropertyName("parent_transaction")]
        public object? ParentTransaction { get; init; }

        [JsonPropertyName("transaction_processed_callback_responses")]
        public IReadOnlyList<object?> TransactionProcessedCallbackResponses {
            get => _transactionProcessedCallbackResponses ?? Array.Empty<object?>();
            init => _transactionProcessedCallbackResponses = value;
        }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }

        public bool IsCreatedSuccessfully() {
            return !Success && Pending;
        }
    }
}
