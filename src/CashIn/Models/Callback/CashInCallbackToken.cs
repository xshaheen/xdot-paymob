// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;

namespace X.Paymob.CashIn.Models.Callback;

[PublicAPI]
public class CashInCallbackToken {
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("token")]
    public string Token { get; init; } = default!;

    [JsonPropertyName("masked_pan")]
    public string MaskedPan { get; init; } = default!;

    [JsonPropertyName("merchant_id")]
    public int MerchantId { get; init; }

    [JsonPropertyName("card_subtype")]
    public string CardSubtype { get; init; } = default!;

    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = default!;

    [JsonPropertyName("email")]
    public string Email { get; init; } = default!;

    [JsonPropertyName("order_id")]
    public string OrderId { get; init; } = default!;

    [JsonPropertyName("user_added")]
    public bool UserAdded { get; init; }

    [JsonExtensionData]
    public IDictionary<string, object?>? ExtensionData { get; init; }

    /// <summary>Return the concatenated string of transaction.</summary>
    public string ToConcatenatedString() {
        return
            CardSubtype +
            CreatedAt +
            Email +
            Id.ToString(CultureInfo.InvariantCulture) +
            MaskedPan +
            MerchantId.ToString(CultureInfo.InvariantCulture) +
            OrderId.ToString(CultureInfo.InvariantCulture) +
            Token;
    }

    public DateTimeOffset CreatedAtDateTimeOffset() {
        var dateTime = DateTime.Parse(CreatedAt, CultureInfo.InvariantCulture);

        // If not have time zone offset consider it cairo time.
        return dateTime.Kind is DateTimeKind.Unspecified
            ? new DateTimeOffset(dateTime, AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter.EgyptTimeZone.GetUtcOffset(dateTime))
            : DateTimeOffset.Parse(CreatedAt, CultureInfo.InvariantCulture);
    }
}
