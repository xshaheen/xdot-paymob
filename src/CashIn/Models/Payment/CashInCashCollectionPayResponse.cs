// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;

namespace X.Paymob.CashIn.Models.Payment {
    [PublicAPI]
    public class CashInCashCollectionPayResponse {
        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("pending")]
        public string Pending { get; init; } = default!;

        [JsonPropertyName("amount_cents")]
        public int AmountCents { get; init; }

        [JsonPropertyName("success")]
        public string Success { get; init; } = default!;

        [JsonPropertyName("is_auth")]
        public string IsAuth { get; init; } = default!;

        [JsonPropertyName("is_capture")]
        public string IsCapture { get; init; } = default!;

        [JsonPropertyName("is_standalone_payment")]
        public string IsStandalonePayment { get; init; } = default!;

        [JsonPropertyName("is_voided")]
        public string IsVoided { get; init; } = default!;

        [JsonPropertyName("is_refunded")]
        public string IsRefunded { get; init; } = default!;

        [JsonPropertyName("is_3d_secure")]
        public string Is3dSecure { get; init; } = default!;

        [JsonPropertyName("integration_id")]
        public int IntegrationId { get; init; }

        [JsonPropertyName("profile_id")]
        public int ProfileId { get; init; }

        [JsonPropertyName("has_parent_transaction")]
        public string HasParentTransaction { get; init; } = default!;

        [JsonPropertyName("order")]
        public int Order { get; init; }

        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset CreatedAt { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonPropertyName("terminal_id")]
        public string? TerminalId { get; init; }

        [JsonPropertyName("merchant_commission")]
        public int MerchantCommission { get; init; }

        [JsonPropertyName("is_void")]
        public string IsVoid { get; init; } = default!;

        [JsonPropertyName("is_refund")]
        public string IsRefund { get; init; } = default!;

        [JsonPropertyName("error_occured")]
        public string ErrorOccured { get; init; } = default!;

        [JsonPropertyName("refunded_amount_cents")]
        public int RefundedAmountCents { get; init; }

        [JsonPropertyName("captured_amount")]
        public int CapturedAmount { get; init; }

        [JsonPropertyName("merchant_staff_tag")]
        public string MerchantStaffTag { get; init; } = default!;

        [JsonPropertyName("owner")]
        public int Owner { get; init; }

        [JsonPropertyName("parent_transaction")]
        public string? ParentTransaction { get; init; }

        [JsonPropertyName("merchant_order_id")]
        public string? MerchantOrderId { get; init; }

        [JsonPropertyName("data.message")]
        public string? DataMessage { get; init; }

        [JsonPropertyName("source_data.type")]
        public string SourceDataType { get; init; } = default!;

        [JsonPropertyName("source_data.pan")]
        public string SourceDataPan { get; init; } = default!;

        [JsonPropertyName("source_data.sub_type")]
        public string SourceDataSubType { get; init; } = default!;

        [JsonPropertyName("acq_response_code")]
        public string? AcqResponseCode { get; init; }

        [JsonPropertyName("txn_response_code")]
        public string? TxnResponseCode { get; init; }

        [JsonPropertyName("hmac")]
        public string Hmac { get; init; } = default!;

        [JsonPropertyName("merchant_txn_ref")]
        public object? MerchantTxnRef { get; init; }

        [JsonPropertyName("use_redirection")]
        public bool UseRedirection { get; init; }

        [JsonPropertyName("redirection_url")]
        public string RedirectionUrl { get; init; } = default!;

        [JsonPropertyName("merchant_response")]
        public string MerchantResponse { get; init; } = default!;

        [JsonPropertyName("bypass_step_six")]
        public bool BypassStepSix { get; init; }
    }
}
