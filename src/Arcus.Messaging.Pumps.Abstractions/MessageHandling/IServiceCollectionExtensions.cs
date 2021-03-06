﻿using System;
using Arcus.Messaging.Abstractions;
using Arcus.Messaging.Pumps.Abstractions;
using Arcus.Messaging.Pumps.Abstractions.MessageHandling;
using GuardNet;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extensions on the <see cref="IServiceCollection"/> to add an general <see cref="IMessageHandler{TMessage,TMessageContext}"/> implementation.
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Adds a <see cref="IMessageHandler{TMessage}" /> implementation to process the messages from an <see cref="MessagePump"/> implementation.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <param name="services">The collection of services to use in the application.</param>
        public static IServiceCollection WithMessageHandler<TMessageHandler, TMessage>(this IServiceCollection services)
            where TMessageHandler : class, IMessageHandler<TMessage, MessageContext>
            where TMessage : class
        {
            Guard.NotNull(services, nameof(services));

            services.AddSingleton<IMessageHandler<TMessage, MessageContext>, TMessageHandler>();

            return services;
        }

        /// <summary>
        /// Adds a <see cref="IMessageHandler{TMessage}" /> implementation to process the messages from an <see cref="MessagePump"/> implementation.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <param name="services">The collection of services to use in the application.</param>
        /// <param name="implementationFactory">The function that creates the service.</param>
        public static IServiceCollection WithMessageHandler<TMessageHandler, TMessage>(
            this IServiceCollection services,
            Func<IServiceProvider, TMessageHandler> implementationFactory)
            where TMessageHandler : class, IMessageHandler<TMessage, MessageContext>
            where TMessage : class
        {
            Guard.NotNull(services, nameof(services));
            Guard.NotNull(implementationFactory, nameof(implementationFactory));

            services.AddSingleton<IMessageHandler<TMessage, MessageContext>, TMessageHandler>(implementationFactory);

            return services;
        }

        /// <summary>
        /// Adds a <see cref="IMessageHandler{TMessage, TMessageContext}" /> implementation to process the messages from an <see cref="MessagePump"/> implementation.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <typeparam name="TMessageContext">The type of the context in which the message handler will process the message.</typeparam>
        /// <param name="services">The collection of services to use in the application.</param>
        public static IServiceCollection WithMessageHandler<TMessageHandler, TMessage, TMessageContext>(this IServiceCollection services)
            where TMessageHandler : class, IMessageHandler<TMessage, TMessageContext> 
            where TMessage : class
            where TMessageContext : MessageContext
        {
            Guard.NotNull(services, nameof(services));

            services.AddSingleton<IMessageHandler<TMessage, TMessageContext>, TMessageHandler>();

            return services;
        }

        /// <summary>
        /// Adds a <see cref="IMessageHandler{TMessage, TMessageContext}" /> implementation to process the messages from an <see cref="MessagePump"/> implementation.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <typeparam name="TMessageContext">The type of the context in which the message handler will process the message.</typeparam>
        /// <param name="services">The collection of services to use in the application.</param>
        /// <param name="implementationFactory">The function that creates the message handler.</param>
        public static IServiceCollection WithMessageHandler<TMessageHandler, TMessage, TMessageContext>(
            this IServiceCollection services,
            Func<IServiceProvider, TMessageHandler> implementationFactory)
            where TMessageHandler : class, IMessageHandler<TMessage, TMessageContext> 
            where TMessage : class
            where TMessageContext : MessageContext
        {
            Guard.NotNull(services, nameof(services));
            Guard.NotNull(implementationFactory, nameof(implementationFactory));

            services.AddSingleton<IMessageHandler<TMessage, TMessageContext>, TMessageHandler>(implementationFactory);

            return services;
        }

        /// <summary>
        /// Adds a <see cref="IMessageHandler{TMessage}" /> implementation to process the messages from an <see cref="MessagePump"/> implementation.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <param name="services">The collection of services to use in the application.</param>
        /// <param name="messageContextFilter">The function that determines if the message handler should handle the message based on the context.</param>
        public static IServiceCollection WithMessageHandler<TMessageHandler, TMessage>(
            this IServiceCollection services,
            Func<MessageContext, bool> messageContextFilter)
            where TMessageHandler : class, IMessageHandler<TMessage, MessageContext>
            where TMessage : class
        {
            Guard.NotNull(services, nameof(services));
            Guard.NotNull(messageContextFilter, nameof(messageContextFilter));

            return services.WithMessageHandler<TMessageHandler, TMessage, MessageContext>(
                messageContextFilter, serviceProvider => ActivatorUtilities.CreateInstance<TMessageHandler>(serviceProvider));
        }

        /// <summary>
        /// Adds a <see cref="IMessageHandler{TMessage, TMessageContext}" /> implementation to process the messages from an <see cref="MessagePump"/> implementation.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <typeparam name="TMessageContext">The type of the context in which the message handler will process the message.</typeparam>
        /// <param name="services">The collection of services to use in the application.</param>
        /// <param name="messageContextFilter">The function that determines if the message handler should handle the message based on the context.</param>
        public static IServiceCollection WithMessageHandler<TMessageHandler, TMessage, TMessageContext>(
            this IServiceCollection services,
            Func<TMessageContext, bool> messageContextFilter)
            where TMessageHandler : class, IMessageHandler<TMessage, TMessageContext>
            where TMessage : class
            where TMessageContext : MessageContext
        {
            Guard.NotNull(services, nameof(services));
            Guard.NotNull(messageContextFilter, nameof(messageContextFilter));

            return services.WithMessageHandler<TMessageHandler, TMessage, TMessageContext>(
                messageContextFilter, serviceProvider => ActivatorUtilities.CreateInstance<TMessageHandler>(serviceProvider));
        }

        /// <summary>
        /// Adds a <see cref="IMessageHandler{TMessage}" /> implementation to process the messages from an <see cref="MessagePump"/> implementation.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <param name="services">The collection of services to use in the application.</param>
        /// <param name="messageContextFilter">The function that determines if the message handler should handle the message based on the context.</param>
        /// <param name="implementationFactory">The function that creates the service.</param>
        public static IServiceCollection WithMessageHandler<TMessageHandler, TMessage>(
            this IServiceCollection services,
            Func<MessageContext, bool> messageContextFilter,
            Func<IServiceProvider, TMessageHandler> implementationFactory)
            where TMessageHandler : class, IMessageHandler<TMessage, MessageContext>
            where TMessage : class
        {
            Guard.NotNull(services, nameof(services));
            Guard.NotNull(messageContextFilter, nameof(messageContextFilter));
            Guard.NotNull(implementationFactory, nameof(implementationFactory));

            return services.WithMessageHandler<TMessageHandler, TMessage, MessageContext>(messageContextFilter, implementationFactory);
        }

        /// <summary>
        /// Adds a <see cref="IMessageHandler{TMessage, TMessageContext}" /> implementation to process the messages from an <see cref="MessagePump"/> implementation.
        /// resources.
        /// </summary>
        /// <typeparam name="TMessageHandler">The type of the implementation.</typeparam>
        /// <typeparam name="TMessage">The type of the message that the message handler will process.</typeparam>
        /// <typeparam name="TMessageContext">The type of the context in which the message handler will process the message.</typeparam>
        /// <param name="services">The collection of services to use in the application.</param>
        /// <param name="messageContextFilter">The function that determines if the message handler should handle the message based on the context.</param>
        /// <param name="implementationFactory">The function that creates the message handler.</param>
        public static IServiceCollection WithMessageHandler<TMessageHandler, TMessage, TMessageContext>(
            this IServiceCollection services,
            Func<TMessageContext, bool> messageContextFilter,
            Func<IServiceProvider, TMessageHandler> implementationFactory)
            where TMessageHandler : class, IMessageHandler<TMessage, TMessageContext>
            where TMessage : class
            where TMessageContext : MessageContext
        {
            Guard.NotNull(services, nameof(services));
            Guard.NotNull(messageContextFilter,  nameof(messageContextFilter));
            Guard.NotNull(implementationFactory, nameof(implementationFactory));

            return services.AddSingleton<IMessageHandler<TMessage, TMessageContext>, MessageHandlerRegistration<TMessage, TMessageContext>>(
                serviceProvider => new MessageHandlerRegistration<TMessage, TMessageContext>(
                    messageContextFilter, implementationFactory(serviceProvider)));
        }
    }
}
