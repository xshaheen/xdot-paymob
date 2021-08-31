// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Collections.Generic;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Payment {
    [PublicAPI]
    public class CashInKioskPayData {
        [JsonPropertyName("gateway_integration_pk")]
        public int GatewayIntegrationPk { get; init; }

        [JsonPropertyName("otp")]
        public string Otp { get; init; } = default!;

        [JsonPropertyName("klass")]
        public string Klass { get; init; } = default!;

        [JsonPropertyName("txn_response_code")]
        public string TxnResponseCode { get; init; } = default!;

        [JsonPropertyName("bill_reference")]
        public int BillReference { get; init; }

        [JsonPropertyName("message")]
        public string Message { get; init; } = default!;

        [JsonPropertyName("paid_through")]
        public string PaidThrough { get; init; } = default!;

        [JsonPropertyName("due_amount")]
        public int DueAmount { get; init; }

        [JsonPropertyName("biller")]
        public object? Biller { get; init; }

        [JsonPropertyName("from_user")]
        public object? FromUser { get; init; }

        [JsonPropertyName("ref")]
        public object? Ref { get; init; }

        [JsonPropertyName("cashout_amount")]
        public object? CashOutAmount { get; init; }

        [JsonPropertyName("agg_terminal")]
        public object? AggTerminal { get; init; }

        [JsonPropertyName("amount")]
        public object? Amount { get; init; }

        [JsonPropertyName("rrn")]
        public object? Rrn { get; init; }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
