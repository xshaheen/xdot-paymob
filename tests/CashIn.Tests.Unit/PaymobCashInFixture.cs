// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using WireMock.Server;
using X.Paymob.CashIn.Models;

namespace CashIn.Tests.Unit;

public sealed class PaymobCashInFixture : IDisposable {
    private readonly ServiceProvider _serviceProvider;

    public PaymobCashInFixture() {
        Server = WireMockServer.Start();
        HttpClient = new HttpClient();
        AutoFixture.Register(() => JsonSerializer.Deserialize<object?>("null"));
        CashInConfig = new CashInConfig { ApiBaseUrl = Server.Urls[0] };
        Options = Substitute.For<IOptionsMonitor<CashInConfig>>();
        Options.CurrentValue.Returns(CashInConfig);
        SystemClock = Substitute.For<ISystemClock>();
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddMemoryCache(options => { options.Clock = SystemClock; });
        _serviceProvider = serviceCollection.BuildServiceProvider();
        MemoryCache = _serviceProvider.GetRequiredService<IMemoryCache>();
    }

    public Fixture AutoFixture { get; } = new();
    public WireMockServer Server { get; }
    public HttpClient HttpClient { get; }
    public CashInConfig CashInConfig { get; }
    public IOptionsMonitor<CashInConfig> Options { get; }
    public ISystemClock SystemClock { get; }
    public IMemoryCache MemoryCache { get; }

    public void Dispose() {
        Server.Stop();
        HttpClient.Dispose();
        _serviceProvider.Dispose();
    }
}
