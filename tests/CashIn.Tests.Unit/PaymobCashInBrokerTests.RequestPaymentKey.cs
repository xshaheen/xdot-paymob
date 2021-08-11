// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models.Payment;
using Xunit;

namespace CashIn.Tests.Unit {
    public partial class PaymobCashInBrokerTests {
        [Fact]
        public async Task should_make_call_and_return_response_when_request_payment_key() {
            // given
            var request = _fixture.AutoFixture.Create<CashInPaymentKeyRequest>();
            var (authenticator, token) = _SetupGentAuthenticationToken();
            var internalRequest = new CashInPaymentKeyInternalRequest(token, request);
            var internalRequestJson = JsonSerializer.Serialize(internalRequest);
            var response = _fixture.AutoFixture.Create<CashInPaymentKeyResponse>();
            var responseJson = JsonSerializer.Serialize(response);

            _fixture.Server
                .Given(Request.Create().WithPath("/acceptance/payment_keys").UsingPost().WithBody(internalRequestJson))
                .RespondWith(Response.Create().WithBody(responseJson));

            // when
            var broker = new PaymobCashInBroker(_fixture.HttpClient, authenticator, _fixture.Options);
            var result = await broker.RequestPaymentKeyAsync(request);

            // then
            JsonSerializer.Serialize(result).Should().Be(responseJson);
            _ = authenticator.Received(1).GetAuthenticationTokenAsync();
        }

        [Fact]
        public void should_throw_http_request_exception_when_request_payment_key_not_success() {
            // given
            var request = _fixture.AutoFixture.Create<CashInPaymentKeyRequest>();
            var (authenticator, _) = _SetupGentAuthenticationToken();

            _fixture.Server
                .Given(Request.Create().WithPath("/acceptance/payment_keys").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError));

            // when
            var broker = new PaymobCashInBroker(_fixture.HttpClient, authenticator, _fixture.Options);
            var invocation = FluentActions.Awaiting(() => broker.RequestPaymentKeyAsync(request));

            // then
            invocation.Should()
                .Throw<HttpRequestException>()
#if NET5_0_OR_GREATER
                .And.StatusCode.Should().Be(HttpStatusCode.InternalServerError)
#endif
                ;
            _ = authenticator.Received(1).GetAuthenticationTokenAsync();
        }
    }
}
