// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

namespace X.Paymob.CashIn; 

public interface IClockBroker {
    long TicksNow { get; }
}