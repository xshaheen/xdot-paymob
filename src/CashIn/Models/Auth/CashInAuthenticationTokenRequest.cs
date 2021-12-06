// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Auth; 

[PublicAPI]
internal class CashInAuthenticationTokenRequest {
    [JsonPropertyName("api_key")]
    public string ApiKey { get; init; } = default!;
}