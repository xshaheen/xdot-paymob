// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

namespace X.Paymob.CashIn.Models.Constants; 

public static class CashInTransactionTypes {
    public const string Auth = "auth";
    public const string Capture = "capture";
    public const string Type3ds = "3ds";
    public const string Refund = "refund";
    public const string Standalone = "standalone";
    public const string Void = "void";
}