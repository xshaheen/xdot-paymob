// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;

namespace X.Paymob.CashIn.Models.Payment {
    [PublicAPI]
    public class CashInWalletData {
        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset CreatedAt { get; init; }

        [JsonPropertyName("gateway_integration_pk")]
        public int GatewayIntegrationPk { get; init; }

        [JsonPropertyName("redirect_url")]
        public string RedirectUrl { get; init; } = default!;

        [JsonPropertyName("klass")]
        public string Klass { get; init; } = default!;

        [JsonPropertyName("mpg_txn_id")]
        public string MpgTxnId { get; init; } = default!;

        [JsonPropertyName("txn_response_code")]
        public string? TxnResponseCode { get; init; }

        [JsonPropertyName("uig_txn_id")]
        public string? UigTxnId { get; init; }

        [JsonPropertyName("upg_txn_id")]
        public string? UpgTxnId { get; init; }

        [JsonPropertyName("token")]
        public string Token { get; init; } = default!;

        [JsonPropertyName("order_info")]
        public string OrderInfo { get; init; } = default!;

        [JsonPropertyName("method")]
        public int Method { get; init; }

        [JsonPropertyName("wallet_issuer")]
        public string WalletIssuer { get; init; } = default!;

        [JsonPropertyName("message")]
        public string Message { get; init; } = default!;

        [JsonPropertyName("amount")]
        public int Amount { get; init; }

        [JsonPropertyName("currency")]
        public string Currency { get; init; } = default!;

        [JsonPropertyName("mer_txn_ref")]
        public string MerTxnRef { get; init; } = default!;

        [JsonPropertyName("upg_qrcode_ref")]
        public string UpgQrcodeRef { get; init; } = default!;

        [JsonPropertyName("wallet_msisdn")]
        public string WalletMsisdn { get; init; } = default!;

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
