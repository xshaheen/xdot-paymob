// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using System.Globalization;
using Ardalis.GuardClauses;
using JetBrains.Annotations;

namespace X.Paymob.CashIn.Models.Payment {
    [PublicAPI]
    public class CashInPaymentKeyRequest {
        public CashInPaymentKeyRequest(
            int integrationId,
            int orderId,
            CashInBillingData billingData,
            int amountCents,
            string currency = "EGP",
            int expiration = 3600,
            bool lockOrderWhenPaid = true
        ) {
            Guard.Against.NegativeOrZero(orderId, nameof(orderId));
            Guard.Against.NegativeOrZero(integrationId, nameof(integrationId));
            Guard.Against.NegativeOrZero(amountCents, nameof(amountCents));
            Guard.Against.NegativeOrZero(expiration, nameof(expiration));
            Guard.Against.NullOrEmpty(currency, nameof(currency));

            OrderId = orderId;
            IntegrationId = integrationId;
            AmountCents = amountCents.ToString(CultureInfo.InvariantCulture);
            Expiration = expiration;
            Currency = currency;
            LockOrderWhenPaid = lockOrderWhenPaid ? "true" : "false";
            BillingData = billingData;
        }

        public int IntegrationId { get; }

        public int OrderId { get; }

        public string AmountCents { get; }

        public int Expiration { get; }

        public string Currency { get; }

        public string LockOrderWhenPaid { get; }

        public CashInBillingData BillingData { get; }
    }
}
