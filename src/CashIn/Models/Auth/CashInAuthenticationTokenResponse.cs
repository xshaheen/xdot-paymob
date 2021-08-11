// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Models.Merchant;

namespace X.Paymob.CashIn.Models.Auth {
    [PublicAPI]
    public class CashInAuthenticationTokenResponse {
        [JsonPropertyName("token")]
        public string Token { get; init; } = default!;

        [JsonPropertyName("profile")]
        public CashInProfile? Profile { get; init; }
    }
}
