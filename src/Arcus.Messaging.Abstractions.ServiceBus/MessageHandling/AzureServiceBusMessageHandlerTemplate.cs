﻿using System;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Arcus.Messaging.Abstractions.ServiceBus.MessageHandling
{
    /// <summary>
    /// Represents the default template when handling Azure Service Bus messages and controlling how the message is being handled by Azure Service Bus.
    /// </summary>
    [Obsolete("Will be removed in v3.0, please implement the " + nameof(IAzureServiceBusMessageHandler<object>) + " directly instead and call the Azure service bus operations on the " + nameof(AzureServiceBusMessageContext) + " to complete/dead-letter/abandon messages")]
    public abstract class AzureServiceBusMessageHandlerTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureServiceBusMessageHandlerTemplate"/> class.
        /// </summary>
        /// <param name="logger">The logger instance to write diagnostic messages during the message handling.</param>
        protected AzureServiceBusMessageHandlerTemplate(ILogger logger)
        {
            Logger = logger ?? NullLogger.Instance;
        }

        /// <summary>
        /// Gets the current event args of the received Azure Service Bus message to run message-specific operations.
        /// </summary>
        internal ProcessMessageEventArgs EventArgs { get; private set; }

        /// <summary>
        /// Gets the logger to write diagnostic messages during the handling of the message.
        /// </summary>
        protected ILogger Logger { get; }

        /// <summary>
        /// Sets the <see cref="ProcessMessageEventArgs"/> instance of the currently received Azure Service Bus message on the message handler template
        /// to prepare to run message-specific operations.
        /// </summary>
        /// <param name="args">The received event args to process the Azure Service Bus message.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="args"/> is <c>null</c>.</exception>
        internal void SetProcessMessageEventArgs(ProcessMessageEventArgs args)
        {
            EventArgs = args ?? throw new ArgumentNullException(nameof(args));
        }
    }
}