// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;

namespace X.Paymob.CashIn {
    public class ClockBroker : IClockBroker {
        public long TicksNow =>
#if NETCOREAPP3_0_OR_GREATER
            Environment.TickCount64;
#else
            Environment.TickCount;
#endif
    }
}
