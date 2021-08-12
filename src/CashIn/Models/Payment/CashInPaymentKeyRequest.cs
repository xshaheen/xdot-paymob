// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using Ardalis.GuardClauses;

namespace X.Paymob.CashIn.Models.Payment {
    public class CashInPaymentKeyRequest {
        public CashInPaymentKeyRequest(
            int integrationId,
            int orderId,
            CashInBillingData billingData,
            int amountCents,
            string currency = "EGP",
            bool lockOrderWhenPaid = true,
            int? expiration = null
        ) {
            if (expiration is not null) {
                Guard.Against.NegativeOrZero(expiration.Value, nameof(expiration));
            }

            Guard.Against.NegativeOrZero(orderId, nameof(orderId));
            Guard.Against.NegativeOrZero(integrationId, nameof(integrationId));
            Guard.Against.NegativeOrZero(amountCents, nameof(amountCents));
            Guard.Against.NullOrEmpty(currency, nameof(currency));
            Guard.Against.Null(billingData, nameof(billingData));

            IntegrationId = integrationId;
            OrderId = orderId;
            AmountCents = amountCents;
            Currency = currency;
            Expiration = expiration;
            LockOrderWhenPaid = lockOrderWhenPaid;
            BillingData = billingData;
        }

        public int IntegrationId { get; }

        public int OrderId { get; }

        public int AmountCents { get; }

        public string Currency { get; }

        public int? Expiration { get; }

        public bool LockOrderWhenPaid { get; }

        public CashInBillingData BillingData { get; }
    }
}
