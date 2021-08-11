// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Constants {
    [PublicAPI]
    public static class CashInStatuses {
        public const string Pending = "pending";
        public const string Declined = "declined";
        public const string Success = "success";
    }
}
