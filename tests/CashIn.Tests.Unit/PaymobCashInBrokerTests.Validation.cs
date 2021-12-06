// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json;
using Microsoft.Extensions.Options;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models;
using X.Paymob.CashIn.Models.Callback;

namespace CashIn.Tests.Unit;

public partial class PaymobCashInBrokerTests {
    public static readonly TheoryData<string, string, string, bool> TransactionData = new() {
        {
            "{\n  \"id\": 11113222,\n  \"amount_cents\": 50000,\n  \"integration_id\": 1111,\n  \"currency\": \"EGP\",\n  \"created_at\": \"2021-08-02T15:45:26.988117\",\n  \"owner\": 1188,\n  \"error_occured\": false,\n  \"has_parent_transaction\": false,\n  \"redirect_url\": null,\n  \"order\": {\n    \"id\": 11112345,\n    \"amount_cents\": 50000,\n    \"paid_amount_cents\": 50000,\n    \"payment_method\": \"tbc\"\n  },\n  \"source_data\": {\n    \"pan\": \"1234\",\n    \"type\": \"card\",\n    \"sub_type\": \"MasterCard\"\n  },\n  \"data\": {\n    \"bill_reference\": 0\n  },\n  \"pending\": false,\n  \"success\": true,\n  \"is_auth\": false,\n  \"is_capture\": false,\n  \"is_voided\": false,\n  \"is_refunded\": false,\n  \"is_3d_secure\": true,\n  \"is_standalone_payment\": true\n}\n",
            "97A5B79200124C9E94E9D73763265D20",
            "62a79c8d174f39d114ded8ad4885af25889b53deeae91d2ff6ddb7202188043e4c492686ce0780c91afc3fd4beb7728d95d7afb64dafa0d522ccacb760cbb950",
            true
        },
        {
            "{\n  \"id\": 11113222,\n  \"amount_cents\": 90000,\n  \"integration_id\": 1111,\n  \"currency\": \"EGP\",\n  \"created_at\": \"2021-08-02T15:45:26.988117\",\n  \"owner\": 1188,\n  \"error_occured\": false,\n  \"has_parent_transaction\": false,\n  \"redirect_url\": null,\n  \"order\": {\n    \"id\": 11112345,\n    \"amount_cents\": 50000,\n    \"paid_amount_cents\": 50000,\n    \"payment_method\": \"tbc\"\n  },\n  \"source_data\": {\n    \"pan\": \"1234\",\n    \"type\": \"card\",\n    \"sub_type\": \"MasterCard\"\n  },\n  \"data\": {\n    \"bill_reference\": 0\n  },\n  \"pending\": false,\n  \"success\": true,\n  \"is_auth\": false,\n  \"is_capture\": false,\n  \"is_voided\": false,\n  \"is_refunded\": false,\n  \"is_3d_secure\": true,\n  \"is_standalone_payment\": true\n}\n",
            "97A5B79200124C9E94E9D73763265D20",
            "62a79c8d174f39d114ded8ad4885af25889b53deeae91d2ff6ddb7202188043e4c492686ce0780c91afc3fd4beb7728d95d7afb64dafa0d522ccacb760cbb950",
            false
        },
    };

    public static readonly TheoryData<string, string, string, bool> TokenData = new() {
        {
            "{ \"id\": 111999, \"token\": \"12ast1f6305b97f7c40f3fffffb699f5452b50881b36b1\", \"masked_pan\": \"xxxx-xxxx-xxxx-1234\", \"merchant_id\": 1112235, \"card_subtype\": \"Visa\", \"created_at\": \"2021-08-04T22:57:48.155912\", \"email\": \"mahmoud@xshaheen.com\", \"order_id\": \"11111231\", \"user_added\": false }",
            "97A5B79200124C9E94E9D73763265D20",
            "af87151d34e0db8855150ea0ce27895601316a72f4d8298d589944d74ccdc3158e93eb9892aaad9693961655793a6cd4498def045a2990f1caf9a39742a0d4c8",
            true
        },
    };

    [Theory]
    [MemberData(nameof(TransactionData))]
    public void should_validate_transaction_hmac_as_expected(string transactionJson, string validationKey, string expectedHmac, bool valid) {
        // given
        var options = Substitute.For<IOptionsMonitor<CashInConfig>>();
        options.CurrentValue.Returns(_ => new CashInConfig { Hmac = validationKey });
        var sut = new PaymobCashInBroker(null!, null!, options);
        var transaction = JsonSerializer.Deserialize<CashInCallbackTransaction>(transactionJson);

        // when
        bool result = sut.Validate(transaction!.ToConcatenatedString(), expectedHmac);

        // then
        result.Should().Be(valid);
    }

    [Theory]
    [MemberData(nameof(TokenData))]
    public void should_validate_token_hmac_as_expected(string tokenJson, string validationKey, string expectedHmac, bool valid) {
        // given
        var options = Substitute.For<IOptionsMonitor<CashInConfig>>();
        options.CurrentValue.Returns(_ => new CashInConfig { Hmac = validationKey });
        var sut = new PaymobCashInBroker(null!, null!, options);
        var token = JsonSerializer.Deserialize<CashInCallbackToken>(tokenJson);

        // when
        bool result = sut.Validate(token!.ToConcatenatedString(), expectedHmac);

        // then
        result.Should().Be(valid);
    }
}
