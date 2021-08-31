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
using X.Paymob.CashIn.Models.Payment;
using Xunit;

namespace CashIn.Tests.Unit {
    public partial class PaymobCashInBrokerTests {
        public static Fixture AutoFixture { get; } = new();

        public static readonly TheoryData<CashInPaymentKeyRequest> RequestPaymentKeyData = new() {
            _GetPaymentKeyRequest(expiration: null),
            _GetPaymentKeyRequest(expiration: AutoFixture.Create<int>()),
        };

        [Theory]
        [MemberData(nameof(RequestPaymentKeyData))]
        public async Task should_make_call_and_return_response_when_request_payment_key(
            CashInPaymentKeyRequest request
        ) {
            // given
            var (authenticator, token) = _SetupGentAuthenticationToken();
            var expiration = _fixture.AutoFixture.Create<int>();
            var config = _fixture.CashInConfig with { ExpirationPeriod = expiration };
            _fixture.Options.CurrentValue.Returns(config);
            var internalRequest = new CashInPaymentKeyInternalRequest(request, token, expiration);
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
        public async Task should_throw_http_request_exception_when_request_payment_key_not_success() {
            // given
            var request = _fixture.AutoFixture.Create<CashInPaymentKeyRequest>();
            var (authenticator, _) = _SetupGentAuthenticationToken();
            var body = _fixture.AutoFixture.Create<string>();

            _fixture.Server
                .Given(Request.Create().WithPath("/acceptance/payment_keys").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError).WithBody(body));

            // when
            var broker = new PaymobCashInBroker(_fixture.HttpClient, authenticator, _fixture.Options);
            var invocation = FluentActions.Awaiting(() => broker.RequestPaymentKeyAsync(request));

            // then
            _ShouldThrowPaymobRequestException(invocation, (int) HttpStatusCode.InternalServerError, body);
            await authenticator.Received(1).GetAuthenticationTokenAsync();
        }

        private static CashInPaymentKeyRequest _GetPaymentKeyRequest(int? expiration) {
            return AutoFixture.Build<CashInPaymentKeyRequest>()
                .FromFactory(() => new CashInPaymentKeyRequest(
                    integrationId: AutoFixture.Create<int>(),
                    orderId: AutoFixture.Create<int>(),
                    billingData: AutoFixture.Create<CashInBillingData>(),
                    amountCents: AutoFixture.Create<int>(),
                    currency: AutoFixture.Create<string>(),
                    lockOrderWhenPaid: AutoFixture.Create<bool>(),
                    expiration: expiration
                )).Create();
        }
    }
}
