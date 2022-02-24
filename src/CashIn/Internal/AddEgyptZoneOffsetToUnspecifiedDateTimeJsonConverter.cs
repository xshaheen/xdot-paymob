// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json;
using System.Text.Json.Serialization;
using TimeZoneConverter;

namespace X.Paymob.CashIn.Internal;

internal sealed class AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter : JsonConverter<DateTimeOffset> {
    public static readonly TimeZoneInfo EgyptTimeZone = TZConvert.GetTimeZoneInfo("Egypt Standard Time");

    public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        DateTime dateTime = reader.GetDateTime();

        // If not have time zone offset consider it cairo time.
        return dateTime.Kind is DateTimeKind.Unspecified
            ? new DateTimeOffset(dateTime, EgyptTimeZone.GetUtcOffset(dateTime))
            : reader.GetDateTimeOffset();
    }

    public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options) {
        writer.WriteStringValue(value.ToOffset(TimeSpan.Zero));
    }
}
