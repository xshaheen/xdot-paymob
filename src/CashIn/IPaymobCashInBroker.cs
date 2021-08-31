// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using X.Paymob.CashIn.Models.Callback;
using X.Paymob.CashIn.Models.Orders;
using X.Paymob.CashIn.Models.Payment;
using X.Paymob.CashIn.Models.Transactions;

namespace X.Paymob.CashIn {
    public interface IPaymobCashInBroker {
        /// <summary>Create order. Order is a logical container for a transaction(s).</summary>
        /// <exception cref="PaymobRequestException"></exception>
        [Pure] Task<CashInCreateOrderResponse> CreateOrderAsync(CashInCreateOrderRequest request);

        /// <summary>
        /// Get payment key which is used to authenticate payment request and verifying transaction
        /// request metadata.
        /// </summary>
        /// <exception cref="PaymobRequestException"></exception>
        [Pure] Task<CashInPaymentKeyResponse> RequestPaymentKeyAsync(CashInPaymentKeyRequest request);

        /// <exception cref="PaymobRequestException"></exception>
        [Pure] Task<CashInWalletPayResponse> CreateWalletPayAsync(string paymentKey, string phoneNumber);

        /// <exception cref="PaymobRequestException"></exception>
        [Pure] Task<CashInKioskPayResponse> CreateKioskPayAsync(string paymentKey);

        /// <exception cref="PaymobRequestException"></exception>
        [Pure] Task<CashInCashCollectionPayResponse> CreateCashCollectionPayAsync(string paymentKey);

        /// <exception cref="PaymobRequestException"></exception>
        [Pure] Task<CashInSavedTokenPayResponse> CreateSavedTokenPayAsync(string paymentKey, string savedToken);

        /// <summary>Get transactions page.</summary>
        /// <exception cref="PaymobRequestException"></exception>
        [Pure] Task<CashInTransactionsPage?> GetTransactionsPageAsync(CashInTransactionsPageRequest? request = null);

        /// <summary>Get transaction by id.</summary>
        /// <exception cref="PaymobRequestException"></exception>
        [Pure] Task<CashInTransaction?> GetTransactionAsync(string transactionId);

        /// <summary>Get order by id.</summary>
        /// <exception cref="PaymobRequestException"></exception>
        [Pure] Task<CashInOrder?> GetOrderAsync(string orderId);

        /// <summary>Get orders page.</summary>
        /// <exception cref="PaymobRequestException"></exception>
        [Pure] Task<CashInOrdersPage?> GetOrdersPageAsync(CashInOrdersPageRequest? request = null);

        /// <summary>Validate the identity and integrity for "Paymob Accept"'s callback submission.</summary>
        /// <param name="concatenatedString">Object concatenated string.</param>
        /// <param name="hmac">Received HMAC.</param>
        /// <returns>True if is valid, otherwise return false.</returns>
        [Pure] bool Validate(string concatenatedString, string hmac);

        /// <summary>Validate the identity and integrity for "Paymob Accept"'s callback submission.</summary>
        /// <param name="transaction">Received transaction.</param>
        /// <param name="hmac">Received HMAC.</param>
        /// <returns>True if is valid, otherwise return false.</returns>
        [Pure] bool Validate(CashInCallbackTransaction transaction, string hmac);

        /// <summary>Validate the identity and integrity for "Paymob Accept"'s callback submission.</summary>
        /// <param name="token">Received token.</param>
        /// <param name="hmac">Received HMAC.</param>
        /// <returns>True if is valid, otherwise return false.</returns>
        [Pure] bool Validate(CashInCallbackToken token, string hmac);

        /// <summary>Create iframe src url.</summary>
        /// <param name="iframeId">Iframe Id.</param>
        /// <param name="token">Payment token.</param>
        [Pure] string CreateIframeSrc(string iframeId, string token);
    }
}
