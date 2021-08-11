// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using Xunit;

namespace CashIn.Tests.Unit {
    public partial class PaymobCashInAuthenticatorTests : IClassFixture<PaymobCashInFixture> {
        private readonly PaymobCashInFixture _fixture;

        public PaymobCashInAuthenticatorTests(PaymobCashInFixture fixture) => _fixture = fixture;
    }
}
