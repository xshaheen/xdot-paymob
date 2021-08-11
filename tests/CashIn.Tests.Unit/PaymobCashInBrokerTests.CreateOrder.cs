// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using Tynamix.ObjectFiller;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models.Orders;
using Xunit;

namespace CashIn.Tests.Unit {
    public partial class PaymobCashInBrokerTests {
        private static readonly JsonSerializerOptions _IgnoreNullOptions =
            new(JsonSerializerDefaults.Web) { IgnoreNullValues = true };

        [Fact]
        public async Task should_make_call_and_return_response_when_create_order() {
            // given
            var request = _CreateOrderRequest();
            var token = _fixture.AutoFixture.Create<string>();
            var authenticator = Substitute.For<IPaymobCashInAuthenticator>();
            authenticator.GetAuthenticationTokenAsync().Returns(token);
            var internalRequest = new CashInCreateOrderInternalRequest(token, request);
            var internalRequestJson = JsonSerializer.Serialize(internalRequest, _IgnoreNullOptions);
            var response = _fixture.AutoFixture.Create<CashInCreateOrderResponse>();
            var responseJson = JsonSerializer.Serialize(response);

            _fixture.Server
                .Given(Request.Create().WithPath("/ecommerce/orders").UsingPost().WithBody(internalRequestJson))
                .RespondWith(Response.Create().WithBody(responseJson));

            // when
            var broker = new PaymobCashInBroker(_fixture.HttpClient, authenticator, _fixture.Options);
            var result = await broker.CreateOrderAsync(request);

            // then
            await authenticator.Received(1).GetAuthenticationTokenAsync();
            JsonSerializer.Serialize(result).Should().Be(responseJson);
        }

        [Fact]
        public void should_throw_http_request_exception_when_create_order_request_not_success() {
            // given
            var request = _CreateOrderRequest();
            var authenticator = Substitute.For<IPaymobCashInAuthenticator>();
            var token = _fixture.AutoFixture.Create<string>();
            authenticator.GetAuthenticationTokenAsync().Returns(token);

            _fixture.Server
                .Given(Request.Create().WithPath("/ecommerce/orders").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError));

            // when
            var broker = new PaymobCashInBroker(_fixture.HttpClient, authenticator, _fixture.Options);
            var invocation = FluentActions.Awaiting(() => broker.CreateOrderAsync(request));

            // then
            invocation.Should()
                .Throw<HttpRequestException>()
#if NET
                .And.StatusCode.Should().Be(HttpStatusCode.InternalServerError)
#endif
                ;
            _ = authenticator.Received(1).GetAuthenticationTokenAsync();
        }

        private static CashInCreateOrderRequest _CreateOrderRequest() {
            return CashInCreateOrderRequest.CreateOrder(
                amountCents: new IntRange(min: 10_000, max: 100_000).GetValue(),
                currency: new MnemonicString(wordCount: 3).GetValue(),
                merchantOrderId: Guid.NewGuid().ToString()
            );
        }
    }
}
