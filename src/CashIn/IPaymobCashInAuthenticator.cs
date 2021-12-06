// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Diagnostics.Contracts;
using X.Paymob.CashIn.Models.Auth;

namespace X.Paymob.CashIn;

public interface IPaymobCashInAuthenticator {
    /// <summary>Request a new authentication token.</summary>
    /// <returns>Authentication token response.</returns>
    /// <exception cref="HttpRequestException"></exception>
    [Pure] Task<CashInAuthenticationTokenResponse> RequestAuthenticationTokenAsync();

    /// <summary>Get authentication token from cache if is valid or request a new one.</summary>
    /// <returns>Authentication token, which is valid for 1 hour from the creation time.</returns>
    /// <exception cref="HttpRequestException"></exception>
    [Pure] ValueTask<string> GetAuthenticationTokenAsync();
}
