// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Orders; 

[PublicAPI]
public class CashInCreateOrderRequestShippingData {
    [JsonPropertyName("first_name")]
    public string FirstName { get; init; } = default!;

    [JsonPropertyName("last_name")]
    public string LastName { get; init; } = default!;

    [JsonPropertyName("email")]
    public string Email { get; init; } = default!;

    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; init; } = default!;

    [JsonPropertyName("country")]
    public string Country { get; init; } = default!;

    [JsonPropertyName("state")]
    public string State { get; init; } = default!;

    [JsonPropertyName("city")]
    public string City { get; init; } = default!;

    [JsonPropertyName("street")]
    public string Street { get; init; } = default!;

    [JsonPropertyName("building")]
    public string Building { get; init; } = default!;

    [JsonPropertyName("floor")]
    public string Floor { get; init; } = default!;

    [JsonPropertyName("apartment")]
    public string Apartment { get; init; } = default!;

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; init; } = default!;

    [JsonPropertyName("extra_description")]
    public string? ExtraDescription { get; init; }
}