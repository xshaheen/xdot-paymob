// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models.Payment;

namespace CashIn.Tests.Unit;

public partial class PaymobCashInBrokerTests {
    [Fact]
    public async Task should_make_call_and_return_response_when_create_kiosk_pay_request() {
        // given
        var requestPaymentKey = GetRandomString;

        var request = new CashInPayRequest {
            Source = CashInSource.Kiosk,
            PaymentToken = requestPaymentKey,
        };

        var requestJson = JsonSerializer.Serialize(request);
        var response = _fixture.AutoFixture.Create<CashInKioskPayResponse>();
        var responseJson = JsonSerializer.Serialize(response);

        _fixture.Server
            .Given(Request.Create().WithPath("/acceptance/payments/pay").UsingPost().WithBody(requestJson))
            .RespondWith(Response.Create().WithBody(responseJson));

        // when
        var broker = new PaymobCashInBroker(_fixture.HttpClient, null!, _fixture.Options);
        var result = await broker.CreateKioskPayAsync(requestPaymentKey);

        // then
        JsonSerializer.Serialize(result).Should().Be(responseJson);
    }
}
