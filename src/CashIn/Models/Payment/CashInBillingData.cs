// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json.Serialization;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Payment; 

[PublicAPI]
public class CashInBillingData {
    public CashInBillingData(
        string firstName,
        string lastName,
        string phoneNumber,
        string email,
        string country = "NA",
        string state = "NA",
        string city = "NA",
        string apartment = "NA",
        string street = "NA",
        string floor = "NA",
        string building = "NA",
        string shippingMethod = "NA",
        string postalCode = "NA"
    ) {
        Guard.Against.NullOrEmpty(firstName, nameof(firstName));
        Guard.Against.NullOrEmpty(lastName, nameof(lastName));
        Guard.Against.NullOrEmpty(email, nameof(email));
        Guard.Against.NullOrEmpty(phoneNumber, nameof(phoneNumber));
        Guard.Against.NullOrEmpty(country, nameof(country));
        Guard.Against.NullOrEmpty(state, nameof(state));
        Guard.Against.NullOrEmpty(city, nameof(city));
        Guard.Against.NullOrEmpty(apartment, nameof(apartment));
        Guard.Against.NullOrEmpty(street, nameof(street));
        Guard.Against.NullOrEmpty(floor, nameof(floor));
        Guard.Against.NullOrEmpty(building, nameof(building));
        Guard.Against.NullOrEmpty(shippingMethod, nameof(shippingMethod));
        Guard.Against.NullOrEmpty(postalCode, nameof(postalCode));

        Email = email;
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Country = country;
        State = state;
        City = city;
        Apartment = apartment;
        Street = street;
        Floor = floor;
        Building = building;
        ShippingMethod = shippingMethod;
        PostalCode = postalCode;
    }

    [JsonPropertyName("email")]
    public string Email { get; init; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; init; }

    [JsonPropertyName("last_name")]
    public string LastName { get; init; }

    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; init; }

    [JsonPropertyName("country")]
    public string Country { get; }

    [JsonPropertyName("state")]
    public string State { get; }

    [JsonPropertyName("city")]
    public string City { get; }

    [JsonPropertyName("apartment")]

    public string Apartment { get; }

    [JsonPropertyName("street")]
    public string Street { get; }

    [JsonPropertyName("floor")]
    public string Floor { get; }

    [JsonPropertyName("building")]
    public string Building { get; }

    [JsonPropertyName("shipping_method")]
    public string ShippingMethod { get; }

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; }
}