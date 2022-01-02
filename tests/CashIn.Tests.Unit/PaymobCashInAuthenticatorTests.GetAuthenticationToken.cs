// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models.Auth;

namespace CashIn.Tests.Unit;

public partial class PaymobCashInAuthenticatorTests {
    [Fact]
    public async Task should_request_new_token_when_cache_token_expired() {
        // given
        _SetupRandomResponse();

        // when
        var authenticator = new PaymobCashInAuthenticator(_fixture.HttpClient, _fixture.MemoryCache, _fixture.Options);
        _fixture.SystemClock.UtcNow.Returns(DateTime.Now);
        string result1 = await authenticator.GetAuthenticationTokenAsync();
        _fixture.SystemClock.UtcNow.Returns(DateTime.Now.AddMinutes(61));
        string result2 = await authenticator.GetAuthenticationTokenAsync();

        // then
        result1.Should().NotBe(result2);
    }

    [Fact]
    public async Task should_cache_token_fo_an_hour_when_success() {
        // given
        _SetupRandomResponse();

        // when
        var authenticator = new PaymobCashInAuthenticator(_fixture.HttpClient, _fixture.MemoryCache, _fixture.Options);
        _fixture.SystemClock.UtcNow.Returns(DateTime.Now);
        string result1 = await authenticator.GetAuthenticationTokenAsync();
        _fixture.SystemClock.UtcNow.Returns(DateTime.Now.AddMinutes(50));
        string result2 = await authenticator.GetAuthenticationTokenAsync();

        // then
        result1.Should().Be(result2);
    }

    private void _SetupRandomResponse() {
        string apiKey = _fixture.AutoFixture.Create<string>();
        var config = _fixture.CashInConfig with { ApiKey = apiKey };
        _fixture.Options.CurrentValue.Returns(config);
        var request = new CashInAuthenticationTokenRequest { ApiKey = apiKey };
        string requestJson = JsonSerializer.Serialize(request);

        _fixture.Server
            .Given(Request.Create().WithPath("/auth/tokens").UsingPost().WithBody(requestJson))
            .RespondWith(Response.Create().WithBody(_ => _GetTokenResponseJson()));
    }

    private string _GetTokenResponseJson() {
        var response = _fixture.AutoFixture.Create<CashInAuthenticationTokenResponse>();
        return JsonSerializer.Serialize(response);
    }
}
