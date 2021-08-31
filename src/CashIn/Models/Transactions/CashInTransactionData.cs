// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;

namespace X.Paymob.CashIn.Models.Transactions {
    [PublicAPI]
    public class CashInTransactionData {
        [JsonPropertyName("refunded_amount")]
        public decimal RefundedAmount { get; init; }

        [JsonPropertyName("amount")]
        public decimal Amount { get; init; }

        [JsonPropertyName("authorised_amount")]
        public decimal AuthorisedAmount { get; init; }

        [JsonPropertyName("captured_amount")]
        public decimal CapturedAmount { get; init; }

        [JsonPropertyName("migs_result")]
        public string MigsResult { get; init; } = default!;

        [JsonPropertyName("avs_result_code")]
        public string AvsResultCode { get; init; } = default!;

        [JsonPropertyName("card_num")]
        public string CardNum { get; init; } = default!;

        [JsonPropertyName("transaction_no")]
        public string TransactionNo { get; init; } = default!;

        [JsonPropertyName("card_type")]
        public string CardType { get; init; } = default!;

        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset CreatedAt { get; init; }

        [JsonPropertyName("batch_no")]
        public string BatchNo { get; init; } = default!;

        [JsonPropertyName("merchant")]
        public string Merchant { get; init; } = default!;

        [JsonPropertyName("acq_response_code")]
        public int AcqResponseCode { get; init; }

        [JsonPropertyName("message")]
        public string Message { get; init; } = default!;

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonPropertyName("authorize_id")]
        public string AuthorizeId { get; init; } = default!;

        [JsonPropertyName("migs_order")]
        public string MigsOrder { get; init; } = default!;

        [JsonPropertyName("order_info")]
        public string OrderInfo { get; init; } = default!;

        /// <summary>WalletPayment | ...</summary>
        [JsonPropertyName("klass")]
        public string Klass { get; init; } = default!;

        [JsonPropertyName("txn_response_code")]
        public string TxnResponseCode { get; init; } = default!;

        [JsonPropertyName("secure_hash")]
        public string SecureHash { get; init; } = default!;

        [JsonPropertyName("merchant_txn_ref")]
        public string MerchantTxnRef { get; init; } = default!;

        [JsonPropertyName("receipt_no")]
        public string ReceiptNo { get; init; } = default!;

        [JsonPropertyName("migs_transaction")]
        public string MigsTransaction { get; init; } = default!;

        [JsonPropertyName("avs_acq_response_code")]
        public string AvsAcqResponseCode { get; init; } = default!;

        [JsonPropertyName("gateway_integration_pk")]
        public int GatewayIntegrationPk { get; init; }

        [JsonPropertyName("redirect_url")]
        public string? RedirectUrl { get; init; }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
