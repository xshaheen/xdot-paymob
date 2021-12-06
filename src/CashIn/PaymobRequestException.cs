// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Net;
using System.Runtime.Serialization;
using Ardalis.GuardClauses;

namespace X.Paymob.CashIn;

[Serializable]
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

    protected PaymobRequestException(SerializationInfo info, StreamingContext streamingContext) : base(info, streamingContext) {
        Guard.Against.Null(info, nameof(info));

        StatusCode = (HttpStatusCode) info.GetInt32(nameof(StatusCode));
        Body = info.GetString(nameof(Body));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
        Guard.Against.Null(info, nameof(info));

        base.GetObjectData(info, context);
        info.AddValue(nameof(StatusCode), (int) StatusCode);
        info.AddValue(nameof(Body), Body);
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
