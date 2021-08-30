// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;

namespace X.Paymob.CashIn.Models.Merchant {
    [PublicAPI]
    public class CashInProfileUser {
        private readonly IReadOnlyList<object?>? _groups;
        private readonly IReadOnlyList<int>? _userPermissions;

        [JsonPropertyName("id")]
        public int Id { get; init; }

        [JsonPropertyName("username")]
        public string Username { get; init; } = default!;

        [JsonPropertyName("first_name")]
        public string FirstName { get; init; } = default!;

        [JsonPropertyName("last_name")]
        public string LastName { get; init; } = default!;

        [JsonPropertyName("date_joined")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset DateJoined { get; init; }

        [JsonPropertyName("email")]
        public string Email { get; init; } = default!;

        [JsonPropertyName("is_active")]
        public bool IsActive { get; init; }

        [JsonPropertyName("is_staff")]
        public bool IsStaff { get; init; }

        [JsonPropertyName("is_superuser")]
        public bool IsSuperuser { get; init; }

        [JsonPropertyName("last_login")]
        [JsonConverter(typeof(AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter))]
        public DateTimeOffset? LastLogin { get; init; }

        [JsonPropertyName("groups")]
        public IReadOnlyList<object?> Groups {
            get => _groups ?? Array.Empty<object?>();
            init => _groups = value;
        }

        [JsonPropertyName("user_permissions")]
        public IReadOnlyList<int> UserPermissions {
            get => _userPermissions ?? Array.Empty<int>();
            init => _userPermissions = value;
        }

        [JsonExtensionData]
        public IDictionary<string, object?>? ExtensionData { get; init; }
    }
}
