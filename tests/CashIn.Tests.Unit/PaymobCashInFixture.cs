// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Net.Http;
using System.Text.Json;
using AutoFixture;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using NSubstitute;
using WireMock.Server;
using X.Paymob.CashIn;
using X.Paymob.CashIn.Models;

namespace CashIn.Tests.Unit {
    [PublicAPI]
    public sealed class PaymobCashInFixture : IDisposable {
        public PaymobCashInFixture() {
            Server = WireMockServer.Start();
            HttpClient = new HttpClient { BaseAddress = new Uri(Server.Urls[0]) };
            AutoFixture.Register(() => JsonSerializer.Deserialize<object?>("null"));
        }

        public Fixture AutoFixture { get; } = new();
        public WireMockServer Server { get; }
        public HttpClient HttpClient { get; }
        public IOptionsMonitor<CashInConfig> Options { get; } = Substitute.For<IOptionsMonitor<CashInConfig>>();
        public IClockBroker ClockBroker { get; } = Substitute.For<IClockBroker>();

        public void Dispose() {
            Server.Stop();
            HttpClient.Dispose();
        }
    }
}
