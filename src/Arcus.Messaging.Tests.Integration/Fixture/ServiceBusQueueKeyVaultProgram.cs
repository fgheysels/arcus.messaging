﻿using System.Collections.Generic;
using Arcus.EventGrid.Publishing;
using Arcus.Messaging.Tests.Core.Messages.v1;
using Arcus.Messaging.Tests.Workers.MessageHandlers;
using Arcus.Security.Core;
using Arcus.Security.Providers.AzureKeyVault;
using Arcus.Security.Providers.AzureKeyVault.Authentication;
using Arcus.Security.Providers.AzureKeyVault.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace Arcus.Messaging.Tests.Workers.ServiceBus
{
    public class ServiceBusQueueKeyVaultProgram
    {
        public static void main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configuration =>
                {
                    configuration.AddCommandLine(args);
                    configuration.AddEnvironmentVariables();
                })
                .ConfigureLogging(loggingBuilder => loggingBuilder.AddConsole(options => options.IncludeScopes = true))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<ISecretProvider>(serviceProvider =>
                    {
                        return new KeyVaultSecretProvider(
                            new ServicePrincipalAuthentication(
                                hostContext.Configuration["ARCUS_KEYVAULT_SERVICEPRINCIPAL_CLIENTID"],
                                hostContext.Configuration["ARCUS_KEYVAULT_SERVICEPRINCIPAL_CLIENTSECRET"]),
                            new KeyVaultConfiguration(
                                hostContext.Configuration["ARCUS_KEYVAULT_VAULTURI"]));
                    });
                    services.AddTransient(svc =>
                    {
                        var configuration = svc.GetRequiredService<IConfiguration>();
                        var eventGridTopic = configuration.GetValue<string>("EVENTGRID_TOPIC_URI");
                        var eventGridKey = configuration.GetValue<string>("EVENTGRID_AUTH_KEY");

                        return EventGridPublisherBuilder
                            .ForTopic(eventGridTopic)
                            .UsingAuthenticationKey(eventGridKey)
                            .Build();
                    });
                    services.AddServiceBusQueueMessagePump(hostContext.Configuration["ARCUS_KEYVAULT_CONNECTIONSTRINGSECRETNAME"])
                            .WithServiceBusMessageHandler<OrdersAzureServiceBusMessageHandler, Order>();

                    services.AddTcpHealthProbes("ARCUS_HEALTH_PORT", builder => builder.AddCheck("sample", () => HealthCheckResult.Healthy()));
                });
    }
}