// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Payment {
    [PublicAPI]
    public class CashInSource {
        public static readonly CashInSource Kiosk = new("AGGREGATOR", "AGGREGATOR");
        public static readonly CashInSource Cash = new("cash", "CASH");
        public static CashInSource Wallet(string phoneNumber) => new(phoneNumber, "WALLET");
        public static CashInSource SavedToken(string savedToken) => new(savedToken, "TOKEN");

        private CashInSource(string identifier, string subtype) {
            Identifier = identifier;
            Subtype = subtype;
        }

        [JsonPropertyName("identifier")]
        public string Identifier { get; init; }

        [JsonPropertyName("subtype")]
        public string Subtype { get; init; }
    }
}
