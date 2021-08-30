// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;

namespace X.Paymob.CashIn.Models.Orders {
    [PublicAPI]
    public class CashInOrderMerchant {
        private readonly IReadOnlyList<string>? _companyEmails;
        private readonly IReadOnlyList<string>? _phones;

        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset CreatedAt { get; init; }

        [JsonPropertyName("company_name")]
        public string CompanyName { get; init; } = string.Empty;

        [JsonPropertyName("state")]
        public string State { get; init; } = string.Empty;

        [JsonPropertyName("country")]
        public string Country { get; init; } = string.Empty;

        [JsonPropertyName("city")]
        public string City { get; init; } = string.Empty;

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; init; } = string.Empty;

        [JsonPropertyName("street")]
        public string Street { get; init; } = string.Empty;

        [JsonPropertyName("phones")]
        public IReadOnlyList<string> Phones {
            get => _phones ?? Array.Empty<string>();
            init => _phones = value;
        }

        [JsonPropertyName("company_emails")]
        public IReadOnlyList<string> CompanyEmails {
            get => _companyEmails ?? Array.Empty<string>();
            init => _companyEmails = value;
        }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
