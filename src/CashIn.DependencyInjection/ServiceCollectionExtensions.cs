// Copyright (c) Mahmoud Shaheen, 2021. All rights reserved.
// Licensed under the Apache 2.0 license.
// See the LICENSE.txt file in the project root for full license information.

using Ardalis.GuardClauses;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using X.Paymob.CashIn.Models;

namespace X.Paymob.CashIn;

public static class ServiceCollectionExtensions {
    /// <summary>Adds services required for using paymob cash in.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="config">The action used to configure <see cref="CashInConfig"/>.</param>
    /// <param name="retryPolicy">Retry policy used to override used policy.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddPaymobCashIn(
        this IServiceCollection services,
        Action<CashInConfig> config,
        Func<PolicyBuilder<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>>? retryPolicy = null
    ) {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(config, nameof(config));

        services.AddOptions<CashInConfig>().PostConfigure(config).ValidateDataAnnotations();
        _AddServices(services, retryPolicy);
        return services;
    }

    /// <summary>Adds services required for using paymob cash in.</summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="cashInSection">The configuration section that contains <see cref="CashInConfig"/> settings.</param>
    /// <param name="retryPolicy">Retry policy used to override used policy.</param>
    /// <returns>The <see cref="IServiceCollection"/> so that additional calls can be chained.</returns>
    public static IServiceCollection AddPaymobCashIn(
        this IServiceCollection services,
        IConfigurationSection cashInSection,
        Func<PolicyBuilder<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>>? retryPolicy = null
    ) {
        Guard.Against.Null(services, nameof(services));
        Guard.Against.Null(cashInSection, nameof(cashInSection));

        services.AddOptions<CashInConfig>().Bind(cashInSection).ValidateDataAnnotations();
        _AddServices(services, retryPolicy);
        return services;
    }

    private static void _AddServices(
        IServiceCollection services,
        Func<PolicyBuilder<HttpResponseMessage>, IAsyncPolicy<HttpResponseMessage>>? retryPolicy
    ) {
        services.AddSingleton<IClockBroker, ClockBroker>();
        const string clientName = "paymob_cash_in";
        services.AddHttpClient(clientName);

        services
            .AddSingleton<IPaymobCashInAuthenticator, PaymobCashInAuthenticator>()
            .AddHttpClient<IPaymobCashInAuthenticator, PaymobCashInAuthenticator>(clientName)
            .AddTransientHttpErrorPolicy(retryPolicy ?? _ConfigurePolicy);

        services
            .AddScoped<IPaymobCashInBroker, PaymobCashInBroker>()
            .AddHttpClient<IPaymobCashInBroker, PaymobCashInBroker>(clientName)
            .AddTransientHttpErrorPolicy(retryPolicy ?? _ConfigurePolicy);
    }

    private static IAsyncPolicy<HttpResponseMessage> _ConfigurePolicy(PolicyBuilder<HttpResponseMessage> builder) {
        return builder.WaitAndRetryAsync(new[] {
            TimeSpan.FromMilliseconds(100),
            TimeSpan.FromMilliseconds(200),
            TimeSpan.FromMilliseconds(400),
            TimeSpan.FromMilliseconds(800),
        });
    }
}
