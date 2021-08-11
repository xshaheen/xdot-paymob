// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Callback {
    [PublicAPI]
    public class CashInCallback {
        /// <summary>See: <see cref="CashInCallbackTypes"/>.</summary>
        [JsonPropertyName("json")]
        public string? Type { get; init; }

        /// <summary>
        /// <para>
        /// If <see cref="Type"/> = <see cref="CashInCallbackTypes.Transaction"/> this will be <see cref="CashInCallbackTransaction"/>
        /// </para>
        /// <para>
        /// If <see cref="Type"/> = <see cref="CashInCallbackTypes.Token"/> this will be <see cref="CashInCallbackToken"/>
        /// </para>
        /// </summary>
        [JsonPropertyName("obj")]
        public JsonElement Obj { get; init; }

        [JsonExtensionData]
        public IDictionary<string, JsonElement>? ExtensionData { get; init; }
    }
}
