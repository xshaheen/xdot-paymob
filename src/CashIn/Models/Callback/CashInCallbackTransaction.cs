// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Text.Json;
using System.Text.Json.Serialization;
using JetBrains.Annotations;
using X.Paymob.CashIn.Internal;
using X.Paymob.CashIn.Models.Payment;

namespace X.Paymob.CashIn.Models.Callback;

[PublicAPI]
public class CashInCallbackTransaction {
    private readonly IReadOnlyList<TransactionProcessedCallbackResponse>? _transactionProcessedCallbackResponses;

    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// It indicating the amount that was paid to this transaction, it might be different than
    /// the original order price, and it is in cents.
    /// </summary>
    [JsonPropertyName("amount_cents")]
    public int AmountCents { get; init; }

    /// <summary>
    /// True in one of these case:
    /// <para>Card Payments: The customer has been redirected to the issuing bank page to enter his OTP.</para>
    /// <para>Kiosk Payments: A payment reference number was generated and it is pending to be paid.</para>
    /// <para>
    /// Cash Payments: Your cash payment is ready to be collected and the courier is on his way to collect it from your
    /// customer.
    /// </para>
    /// </summary>
    [JsonPropertyName("pending")]
    public bool Pending { get; init; }

    /// <summary>
    /// A boolean-valued key indicating the status of the transaction whether it was successful
    /// or not, it would be true if your customer has successfully performed his payment.
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; init; }

    /// <summary>
    /// A boolean-valued key indicating if this was an authorized transaction, learn more about
    /// auth/cap transactions.
    /// </summary>
    [JsonPropertyName("is_auth")]
    public bool IsAuth { get; init; }

    /// <summary>
    /// A boolean-valued key indicating if this was a capture transaction, learn more about
    /// auth/cap transactions.
    /// </summary>
    [JsonPropertyName("is_capture")]
    public bool IsCapture { get; init; }

    /// <summary>
    /// A boolean-valued key indicating if this transaction was voided or not, learn more about
    /// void and refund transaction.
    /// </summary>
    [JsonPropertyName("is_voided")]
    public bool IsVoided { get; init; }

    /// <summary>
    /// A boolean-valued key indicating if this transaction was refunded or not, learn more
    /// about void and refund transaction.
    /// </summary>
    [JsonPropertyName("is_refunded")]
    public bool IsRefunded { get; init; }

    /// <summary>
    /// A boolean-valued key indicating if this transaction was 3D secured or not, learn more
    /// about the 3D and moto transactions.
    /// </summary>
    [JsonPropertyName("is_3d_secure")]
    public bool Is3dSecure { get; init; }

    [JsonPropertyName("is_standalone_payment")]
    public bool IsStandalonePayment { get; init; }

    [JsonPropertyName("integration_id")]
    public int IntegrationId { get; init; }

    [JsonPropertyName("profile_id")]
    public int ProfileId { get; init; }

    [JsonPropertyName("has_parent_transaction")]
    public bool HasParentTransaction { get; init; }

    [JsonPropertyName("created_at")]
    public string CreatedAt { get; init; } = default!;

    [JsonPropertyName("currency")]
    public string Currency { get; init; } = default!;

    [JsonPropertyName("api_source")]
    public string ApiSource { get; init; } = default!;

    [JsonPropertyName("merchant_commission")]
    public int MerchantCommission { get; init; }

    [JsonPropertyName("is_void")]
    public bool IsVoid { get; init; }

    [JsonPropertyName("is_refund")]
    public bool IsRefund { get; init; }

    [JsonPropertyName("is_hidden")]
    public bool IsHidden { get; init; }

    [JsonPropertyName("error_occured")]
    public bool ErrorOccured { get; init; }

    [JsonPropertyName("is_live")]
    public bool IsLive { get; init; }

    [JsonPropertyName("refunded_amount_cents")]
    public int RefundedAmountCents { get; init; }

    [JsonPropertyName("source_id")]
    public int SourceId { get; init; }

    [JsonPropertyName("is_captured")]
    public bool IsCaptured { get; init; }

    [JsonPropertyName("captured_amount")]
    public int CapturedAmount { get; init; }

    [JsonPropertyName("owner")]
    public int Owner { get; init; }

    [JsonPropertyName("terminal_id")]
    public string? TerminalId { get; init; }

    [JsonPropertyName("data")]
    public CashInCallbackTransactionData? Data { get; init; }

    [JsonPropertyName("order")]
    public CashInCallbackTransactionOrder Order { get; init; } = default!;

    [JsonPropertyName("payment_key_claims")]
    public CashInPayPaymentKeyClaims? PaymentKeyClaims { get; init; }

    [JsonPropertyName("source_data")]
    public CashInCallbackTransactionSourceData? SourceData { get; init; }

    [JsonPropertyName("transaction_processed_callback_responses")]
    public IReadOnlyList<TransactionProcessedCallbackResponse> TransactionProcessedCallbackResponses {
        get => _transactionProcessedCallbackResponses ?? Array.Empty<TransactionProcessedCallbackResponse>();
        init => _transactionProcessedCallbackResponses = value;
    }

    [JsonPropertyName("other_endpoint_reference")]
    public object? OtherEndpointReference { get; init; }

    [JsonPropertyName("merchant_staff_tag")]
    public object? MerchantStaffTag { get; init; }

    [JsonPropertyName("parent_transaction")]
    public int? ParentTransaction { get; init; }

    [JsonExtensionData]
    public IDictionary<string, object?>? ExtensionData { get; init; }

    /// <summary>Return the concatenated string of transaction.</summary>
    public string ToConcatenatedString() {
        static string toString(bool value) => value ? "true" : "false";

        return
            AmountCents.ToString(CultureInfo.InvariantCulture) +
            CreatedAt +
            Currency +
            toString(ErrorOccured) +
            toString(HasParentTransaction) +
            Id.ToString(CultureInfo.InvariantCulture) +
            IntegrationId.ToString(CultureInfo.InvariantCulture) +
            toString(Is3dSecure) +
            toString(IsAuth) +
            toString(IsCapture) +
            toString(IsRefunded) +
            toString(IsStandalonePayment) +
            toString(IsVoided) +
            Order.Id.ToString(CultureInfo.InvariantCulture) +
            Owner.ToString(CultureInfo.InvariantCulture) +
            toString(Pending) +
            SourceData?.Pan?.ToLowerInvariant() +
            SourceData?.SubType +
            SourceData?.Type?.ToLowerInvariant() +
            toString(Success);
    }

    public DateTimeOffset CreatedAtDateTimeOffset() {
        var dateTime = DateTime.Parse(CreatedAt, CultureInfo.InvariantCulture);

        // If not have time zone offset consider it cairo time.
        return dateTime.Kind is DateTimeKind.Unspecified
            ? new DateTimeOffset(dateTime, AddEgyptZoneOffsetToUnspecifiedDateTimeJsonConverter.EgyptTimeZone.GetUtcOffset(dateTime))
            : DateTimeOffset.Parse(CreatedAt, CultureInfo.InvariantCulture);
    }

    public bool IsCard() => SourceData?.Type == "card";

    public bool IsWallet() => SourceData?.Type == "wallet";

    public bool IsCashCollection() => SourceData?.Type == "cash_present";

    public bool IsAcceptKiosk() => SourceData?.Type == "aggregator";

    public bool IsFromIFrame() => ApiSource == "IFRAME";

    public bool IsInvoice() => ApiSource == "INVOICE";

    public bool IsInsufficientFundError() => Data?.TxnResponseCode == "INSUFFICIENT_FUNDS";

    public bool IsAuthenticationFailedError() => Data?.TxnResponseCode == "AUTHENTICATION_FAILED";

    public bool IsDeclinedError() {
        // "data.message": may be  "Do not honour", or "Invalid card number", ...
        return Data?.TxnResponseCode == "DECLINED";
    }

    public bool IsRiskChecksError() {
        return
            Data?.TxnResponseCode == "11" &&
            Data?.Message is not null &&
            Data.Message.ToLowerInvariant().Contains("transaction did not pass risk checks");
    }

    public (string CardNumber, string? Type, string? Bank)? Card() {
        if (!IsCard()) {
            return null;
        }

        var last4 = Data?.CardNum ?? SourceData?.Pan ?? "xxxx";
        var bank = _GetFromExtensions("card_holder_bank") ?? "Other";
        var type = Data?.CardType ?? SourceData?.SubType ?? _GetFromExtensions("card_type");

        type = type?.ToUpperInvariant() switch {
            "MASTERCARD" => "MasterCard",
            "VISA" => "Visa",
            _ => type,
        };

        return (last4, type, bank == "-" ? null : bank);
    }

    private string? _GetFromExtensions(string name) {
        return ExtensionData is null
            ? null
            : ExtensionData.TryGetValue(name, out var value) ? ((JsonElement?) value)?.GetString() : null;
    }
}
