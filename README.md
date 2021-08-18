# Xdot.Paymob

An SDK for .NET to help you integrate with the Paymob’s payment gateway.

**if you like this work, please consider give the project star 🌟**

## Features

- Supporting .NET Standard 2.0+, .NET 5+, .NET Core 2.0+, and .NET Framework 4.6.1+.
- Automatic retries - The library automatically retries requests on intermittent failures.
- Manage authentication tokens - The library manage authenticate (using the configured ApiKey), token caching and invalidation. 
  so you don't need to provide it for each request and the library provide it in each request need it for you.
- You can access the full response property.
- Handle API DateTime responses correctly - all responses use `DateTimeOffset`.
- Ability to swap the configuration (API key, hmac, ...) during runtime.

## Installation

Using the [.NET CLI tools][dotnet-core-cli-tools]:

```sh
dotnet add package Xdot.Paymob.CashIn.DependencyInjection
```

Using the [NuGet CLI][nuget-cli]:

```sh
nuget install Xdot.Paymob.CashIn.DependencyInjection
```

Using the [Package Manager Console][package-manager-console]:

```powershell
Install-Package Xdot.Paymob.CashIn.DependencyInjection
```

## Usage

### Configuration Dependency Injection

Configure the library in `Startup.cs` with these helper methods. This will inject `IPaymobCashInBroker` (used to call
the Paymob API),
`IPaymobCashInAuthenticator` (used to authenticate and manage authentication token), and configure options.

```c#
services.AddPaymobCashIn(config => {
    config.ApiKey = "Api Key";
    config.Hmac = "Hmac secret",
});

// Alert: ApiKey and Hmac is a sensitive settings make sure to store them into
// a secret manager (Azure key vault for example).
// DON'T STORE SECRETS IN CODE
```

- If you don't use Microsoft DI, Use the base package [Xdot.Paymob.CashIn][cash-in-package] and configure your DI to the
  equivalent to [this configuration][di-config-ref].

---

- Then you can inject `IPaymobCashInBroker` to your service and use to to call the Paymob API.

Here's the details of what the `IPaymobCashInBroker` has to offer:

| Method                       | Description                                                                                                                          |
| ---------------------------- | ------------------------------------------------------------------------------------------------------------------------------------ |
| CreateOrderAsync             | See: https://docs.paymob.com/docs/accept-standard-redirect#2-order-registration-api                                                  |
| RequestPaymentKeyAsync       | See: https://docs.paymob.com/docs/accept-standard-redirect#3-payment-key-request                                                     |
| CreateWalletPayAsync         | See: https://docs.paymob.com/docs/mobile-wallets#pay-request                                                                         |
| CreateKioskPayAsync          | See: https://docs.paymob.com/docs/kiosk-payments                                                                                     |
| CreateCashCollectionPayAsync | See: https://docs.paymob.com/docs/cash-collection                                                                                    |
| CreateSavedTokenPayAsync     | See: https://docs.paymob.com/docs/pay-with-saved-token                                                                               |
| GetTransactionAsync          | Get transaction by id.                                                                                                               |
| GetTransactionsPageAsync     | Get transactions page.                                                                                                               |
| GetOrderAsync                | Get an order by id.                                                                                                                  |
| GetOrdersPageAsync           | Get orders page.                                                                                                                     |
| CreateIframeSrc              | Helper method to create iframe src url                                                                                               |
| Validate                     | Helper method to verify callback content with your hmac secret see: https://docs.paymob.com/docs/transaction-webhooks#hmac-authentication |

### Simple Example

```c#
public class CashInService
{
    private readonly IPaymobCashInBroker _broker;

    public CashInService(IPaymobCashInBroker broker)
    {
        _broker = broker;
    }

    public async Task<string> RequestCardPaymentKey()
    {
        // Create order.
        var amountCents = 1000; // 10 LE
        var orderRequest = CashInCreateOrderRequest.CreateOrder(amountCents);
        var orderResponse = await _broker.CreateOrderAsync(orderRequest);

        // Request card payment key.
        var billingData = new CashInBillingData(
            firstName: "Mahmoud",
            lastName: "Shaheen",
            phoneNumber: "010000000",
            email: "mxshaheen@gmail.com");

        var paymentKeyRequest = new CashInPaymentKeyRequest(
            integrationId: 123,
            orderId: orderResponse.Id,
            billingData: billingData,
            amountCents: amountCents);

        var paymentKeyResponse = await _broker.RequestPaymentKeyAsync(paymentKeyRequest);

        // Create iframe src.
        return _broker.CreateIframeSrc(iframeId: "1234", token: paymentKeyResponse.PaymentKey);
    }
}
```

## License

This project is licensed under the Apache 2.0 license.

## Contact

If you have any suggestions, comments or questions, please feel free to contact me on:

Email: mxshaheen@gmail.com

[cash-in-package]: https://www.nuget.org/packages/Xdot.Paymob.CashIn/
[dotnet-core-cli-tools]: https://docs.microsoft.com/en-us/dotnet/core/tools/
[nuget-cli]: https://docs.microsoft.com/en-us/nuget/tools/nuget-exe-cli-reference
[package-manager-console]: https://docs.microsoft.com/en-us/nuget/tools/package-manager-console
[di-config-ref]: https://github.com/xshaheen/xdot-paymob/blob/main/src/CashIn.DependencyInjection/ServiceCollectionExtensions.cs#L58
