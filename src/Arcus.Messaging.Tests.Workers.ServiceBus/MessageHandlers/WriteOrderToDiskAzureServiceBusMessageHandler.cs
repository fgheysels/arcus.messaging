﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Arcus.Messaging.Abstractions;
using Arcus.Messaging.Abstractions.ServiceBus;
using Arcus.Messaging.Abstractions.ServiceBus.MessageHandling;
using Arcus.Messaging.Tests.Core.Events.v1;
using Arcus.Messaging.Tests.Core.Messages.v1;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Xunit;

namespace Arcus.Messaging.Tests.Workers.MessageHandlers
{
    public class WriteOrderToDiskAzureServiceBusMessageHandler : AzureServiceBusMessageHandler<Order>
    {
        private readonly IMessageCorrelationInfoAccessor _correlationAccessor;
        private readonly ILogger<WriteOrderToDiskAzureServiceBusMessageHandler> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteOrderToDiskAzureServiceBusMessageHandler" /> class.
        /// </summary>
        public WriteOrderToDiskAzureServiceBusMessageHandler(
            IMessageCorrelationInfoAccessor correlationAccessor,
            ILogger<WriteOrderToDiskAzureServiceBusMessageHandler> logger) 
            : base(logger)
        {
            _correlationAccessor = correlationAccessor;
            _logger = logger;
        }

        public override async Task ProcessMessageAsync(
            Order message,
            AzureServiceBusMessageContext messageContext,
            MessageCorrelationInfo correlationInfo,
            CancellationToken cancellationToken)
        {
            EnsureSameCorrelation(correlationInfo);

            _logger.LogTrace("Write order v1 message to disk: {MessageId}", message.Id);

            string fileName = message.Id + ".json";
            string dirPath = Directory.GetCurrentDirectory();
            string filePath = Path.Combine(dirPath, fileName);

            string json = JsonConvert.SerializeObject(
                new OrderCreatedEventData(
                    message.Id,
                    message.Amount,
                    message.ArticleNumber,
                    message.Customer.FirstName + " " + message.Customer.LastName,
                    correlationInfo));

            await File.WriteAllTextAsync(filePath, json, cancellationToken);
        }

        private void EnsureSameCorrelation(MessageCorrelationInfo correlationInfo)
        {
            MessageCorrelationInfo registeredCorrelation = _correlationAccessor.GetCorrelationInfo();
            Assert.NotNull(registeredCorrelation);
            Assert.Equal(registeredCorrelation.OperationId, correlationInfo.OperationId);
            Assert.Equal(registeredCorrelation.TransactionId, correlationInfo.TransactionId);
            Assert.Equal(registeredCorrelation.OperationParentId, correlationInfo.OperationParentId);
            Assert.Equal(registeredCorrelation.CycleId, correlationInfo.CycleId);
        }
    }
}