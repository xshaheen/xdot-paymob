// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using NSubstitute;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models.Auth;
using Xunit;

namespace CashIn.Tests.Unit {
    public partial class PaymobCashInAuthenticatorTests {
        [Fact]
        public async Task should_make_call_and_return_response_when_send_request() {
            // given
            string apiKey = _fixture.AutoFixture.Create<string>();
            var config = _fixture.CashInConfig with { ApiKey = apiKey };
            _fixture.Options.CurrentValue.Returns(config);
            var request = new CashInAuthenticationTokenRequest { ApiKey = apiKey };
            string requestJson = JsonSerializer.Serialize(request);
            var expectedResponse = _fixture.AutoFixture.Create<CashInAuthenticationTokenResponse>();
            string expectedResponseJson = JsonSerializer.Serialize(expectedResponse);

            _fixture.Server
                .Given(Request.Create().WithPath("/auth/tokens").UsingPost().WithBody(requestJson))
                .RespondWith(Response.Create().WithBody(expectedResponseJson));

            // when
            var authenticator = new PaymobCashInAuthenticator(
                _fixture.HttpClient,
                _fixture.ClockBroker,
                _fixture.Options
            );

            var result = await authenticator.RequestAuthenticationTokenAsync();

            // then
            JsonSerializer.Serialize(result).Should().BeEquivalentTo(expectedResponseJson);
        }

        [Fact]
        public void should_throw_http_request_exception_when_not_success() {
            // given
            string apiKey = _fixture.AutoFixture.Create<string>();
            var config = _fixture.CashInConfig with { ApiKey = apiKey };
            _fixture.Options.CurrentValue.Returns(config);
            var request = new CashInAuthenticationTokenRequest { ApiKey = apiKey };
            string requestJson = JsonSerializer.Serialize(request);
            var body = _fixture.AutoFixture.Create<string>();

            _fixture.Server
                .Given(Request.Create().WithPath("/auth/tokens").UsingPost().WithBody(requestJson))
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError).WithBody(body));

            // when
            var authenticator =
                new PaymobCashInAuthenticator(_fixture.HttpClient, _fixture.ClockBroker, _fixture.Options);
            var invocation = FluentActions.Awaiting(() => authenticator.RequestAuthenticationTokenAsync());

            // then
            var exception = invocation.Should().Throw<PaymobRequestException>().Which;
            exception.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            exception.Message.Should().Be("Paymob Cash In - Http request failed with status code (500).");
            exception.Body.Should().Be(body);
        }
    }
}
