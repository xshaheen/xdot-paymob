// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using X.Paymob.CashIn;
using Xunit;

namespace CashIn.Tests.Unit {
    public partial class PaymobCashInBrokerTests {
        public static readonly TheoryData<Func<PaymobCashInBroker, Task<object>>> PayRequests = new() {
            async broker => await broker.CreateWalletPayAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
            async broker => await broker.CreateKioskPayAsync(Guid.NewGuid().ToString()),
            async broker => await broker.CreateCashCollectionPayAsync(Guid.NewGuid().ToString()),
            async broker => await broker.CreateSavedTokenPayAsync(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
        };

        [Theory]
        [MemberData(nameof(PayRequests))]
        public void should_throw_http_request_exception_when_create_pay_request_not_success(
            Func<PaymobCashInBroker, Task<object>> func
        ) {
            // given
            _fixture.Server
                .Given(Request.Create().WithPath("/acceptance/payments/pay").UsingPost())
                .RespondWith(Response.Create().WithStatusCode(HttpStatusCode.InternalServerError));

            // when
            var broker = new PaymobCashInBroker(_fixture.HttpClient, null!, _fixture.Options);
            var invocation = FluentActions.Awaiting(() => func(broker));

            // then
            invocation.Should()
                .Throw<HttpRequestException>()
#if NET5_0_OR_GREATER
                .And.StatusCode.Should().Be(HttpStatusCode.InternalServerError)
#endif
                ;
        }
    }
}
