// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System;
using System.Security.Cryptography;
using System.Text;
using Ardalis.GuardClauses;

namespace X.Paymob.CashIn {
    public partial class PaymobCashInBroker {
        public bool Validate(string concatenatedString, string hmac) {
            Guard.Against.NullOrEmpty(concatenatedString, nameof(concatenatedString));
            Guard.Against.NullOrEmpty(hmac, nameof(hmac));

            byte[] textBytes = Encoding.UTF8.GetBytes(concatenatedString);
            byte[] keyBytes = Encoding.UTF8.GetBytes(_config.Hmac);
            byte[] hashBytes = _GetHashBytes(textBytes, keyBytes);
            string lowerCaseHexHash = _ToLowerCaseHex(hashBytes);
            return lowerCaseHexHash.Equals(hmac, StringComparison.Ordinal);
        }

        private static string _ToLowerCaseHex(byte[] hashBytes) {
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }

        private static byte[] _GetHashBytes(byte[] textBytes, byte[] keyBytes) {
            using var hash = new HMACSHA512(keyBytes);
            return hash.ComputeHash(textBytes);
        }
    }
}
