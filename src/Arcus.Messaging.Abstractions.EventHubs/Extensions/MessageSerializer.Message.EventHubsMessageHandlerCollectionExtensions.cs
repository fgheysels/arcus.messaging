﻿using System;
using Arcus.Messaging.Abstractions.EventHubs;
using Arcus.Messaging.Abstractions.EventHubs.MessageHandling;
using Arcus.Messaging.Abstractions.MessageHandling;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions on the <see cref="IServiceCollection"/> to add an <see cref="IAzureEventHubsMessageHandler{TMessage}"/>'s implementations.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static partial class EventHubsMessageHandlerCollectionExtensions
    {
        /// <summary>
        /// Adds a <see cref="IAzureEventHubsMessageHandler{TMessage}" /> implementation to process the messages from an Azure EventHubs.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <param name="handlers">The collection of handlers to use in the application.</param>
        /// <param name="messageBodySerializer">The custom <see cref="IMessageBodySerializer"/> to deserialize the incoming message for the <see cref="IAzureEventHubsMessageHandler{TMessage}"/>.</param>
        /// <param name="messageBodyFilter">The filter to restrict the message processing based on the incoming message body.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="handlers"/>, <paramref name="messageBodySerializer"/>, or <paramref name="messageBodyFilter"/> is <c>null</c>.</exception>
        public static EventHubsMessageHandlerCollection WithEventHubsMessageHandler<TMessageHandler, TMessage>(
            this EventHubsMessageHandlerCollection handlers,
            IMessageBodySerializer messageBodySerializer,
            Func<TMessage, bool> messageBodyFilter)
            where TMessageHandler : class, IAzureEventHubsMessageHandler<TMessage>
            where TMessage : class
        {
            if (handlers is null)
            {
                throw new ArgumentNullException(nameof(handlers));
            }

            handlers.WithMessageHandler<TMessageHandler, TMessage, AzureEventHubsMessageContext>(messageBodySerializer, messageBodyFilter);
            return handlers;
        }

        /// <summary>
        /// Adds a <see cref="IAzureEventHubsMessageHandler{TMessage}" /> implementation to process the messages from an Azure EventHubs.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <param name="handlers">The collection of handlers to use in the application.</param>
        /// <param name="messageBodySerializer">The custom <see cref="IMessageBodySerializer"/> to deserialize the incoming message for the <see cref="IAzureEventHubsMessageHandler{TMessage}"/>.</param>
        /// <param name="messageBodyFilter">The filter to restrict the message processing based on the incoming message body.</param>
        /// <param name="implementationFactory">The function that creates the message handler.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="handlers"/>, <paramref name="messageBodySerializer"/>, <paramref name="messageBodyFilter"/>, or <paramref name="implementationFactory"/> is <c>null</c>.</exception>
        public static EventHubsMessageHandlerCollection WithEventHubsMessageHandler<TMessageHandler, TMessage>(
            this EventHubsMessageHandlerCollection handlers,
            Func<TMessage, bool> messageBodyFilter,
            IMessageBodySerializer messageBodySerializer,
            Func<IServiceProvider, TMessageHandler> implementationFactory)
            where TMessageHandler : class, IAzureEventHubsMessageHandler<TMessage>
            where TMessage : class
        {
            if (handlers is null)
            {
                throw new ArgumentNullException(nameof(handlers));
            }

            handlers.WithMessageHandler<TMessageHandler, TMessage, AzureEventHubsMessageContext>(messageBodySerializer, messageBodyFilter, implementationFactory);
            return handlers;
        }

        /// <summary>
        /// Adds a <see cref="IAzureEventHubsMessageHandler{TMessage}" /> implementation to process the messages from an Azure EventHubs.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <param name="handlers">The collection of handlers to use in the application.</param>
        /// <param name="messageBodySerializerImplementationFactory">The function to create the custom <see cref="IMessageBodySerializer"/> to deserialize the incoming message for the <see cref="IAzureEventHubsMessageHandler{TMessage}"/>.</param>
        /// <param name="messageBodyFilter">The filter to restrict the message processing based on the incoming message body.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="handlers"/>, <paramref name="messageBodySerializerImplementationFactory"/>, or <paramref name="messageBodyFilter"/> is <c>null</c>.</exception>
        public static EventHubsMessageHandlerCollection WithEventHubsMessageHandler<TMessageHandler, TMessage>(
            this EventHubsMessageHandlerCollection handlers,
            Func<IServiceProvider, IMessageBodySerializer> messageBodySerializerImplementationFactory,
            Func<TMessage, bool> messageBodyFilter)
            where TMessageHandler : class, IAzureEventHubsMessageHandler<TMessage>
            where TMessage : class
        {
            if (handlers is null)
            {
                throw new ArgumentNullException(nameof(handlers));
            }

            handlers.WithMessageHandler<TMessageHandler, TMessage, AzureEventHubsMessageContext>(messageBodySerializerImplementationFactory, messageBodyFilter);
            return handlers;
        }

        /// <summary>
        /// Adds a <see cref="IAzureEventHubsMessageHandler{TMessage}" /> implementation to process the messages from an Azure EventHubs.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <param name="handlers">The collection of handlers to use in the application.</param>
        /// <param name="messageBodySerializerImplementationFactory">The function to create the custom <see cref="IMessageBodySerializer"/> to deserialize the incoming message for the <see cref="IAzureEventHubsMessageHandler{TMessage}"/>.</param>
        /// <param name="messageBodyFilter">The filter to restrict the message processing based on the incoming message body.</param>
        /// <param name="messageHandlerImplementationFactory">The function that creates the message handler.</param>
        /// <exception cref="ArgumentNullException">Thrown when the <paramref name="handlers"/>, <paramref name="messageBodySerializerImplementationFactory"/>, <paramref name="messageBodyFilter"/>, or <paramref name="messageHandlerImplementationFactory"/> is <c>null</c>.</exception>
        public static EventHubsMessageHandlerCollection WithEventHubsMessageHandler<TMessageHandler, TMessage>(
            this EventHubsMessageHandlerCollection handlers,
            Func<TMessage, bool> messageBodyFilter,
            Func<IServiceProvider, IMessageBodySerializer> messageBodySerializerImplementationFactory,
            Func<IServiceProvider, TMessageHandler> messageHandlerImplementationFactory)
            where TMessageHandler : class, IAzureEventHubsMessageHandler<TMessage>
            where TMessage : class
        {
            if (handlers is null)
            {
                throw new ArgumentNullException(nameof(handlers));
            }

            handlers.WithMessageHandler<TMessageHandler, TMessage, AzureEventHubsMessageContext>(messageBodySerializerImplementationFactory, messageBodyFilter, messageHandlerImplementationFactory);
            return handlers;
        }
    }
}
