// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net;

namespace X.Paymob.CashIn;

public class PaymobRequestException : Exception {
    /// <summary>Gets the HTTP response status code.</summary>
    /// <value>An HTTP status code.</value>
    public HttpStatusCode StatusCode { get; }

    /// <summary>Gets the HTTP response body if presented.</summary>
    /// <value>An HTTP body.</value>
    public string? Body { get; }

    public PaymobRequestException(string? message, HttpStatusCode statusCode, string? body) : base(message) {
        StatusCode = statusCode;
        Body = body;
    }

    public static async Task ThrowAsync(HttpResponseMessage response) {
        if (response.IsSuccessStatusCode) {
            return;
        }

        string? body;

        try {
            body = await response.Content.ReadAsStringAsync();
        }
        catch {
            body = null;
        }

        response.Dispose();

        var message = string.Format(
            CultureInfo.InvariantCulture,
            "Paymob Cash In - Http request failed with status code ({0}).",
            (int) response.StatusCode
        );
        throw new PaymobRequestException(message, response.StatusCode, body);
    }
}
