using System;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Processor;
using Azure.Messaging.EventHubs.Producer;
using Azure.Storage.Blobs;
using Cocona;

namespace AzureEventHubCli
{
    public class Program
    {
        public static Task Main(string[] args) => CoconaLiteApp.RunAsync<Program>(args);

        [Command("send")]
        public async Task SendAsync(string connectionString, string eventHubName, string eventBody)
        {
            // Create a producer client that you can use to send events to an event hub
            await using var producerClient = new EventHubProducerClient(connectionString, eventHubName);

            // Create a batch of events
            using EventDataBatch eventBatch = await producerClient.CreateBatchAsync();

            // Add events to the batch. An event is a represented by a collection of bytes and metadata.
            eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(eventBody)));

            // Use the producer client to send the batch of events to the event hub
            await producerClient.SendAsync(eventBatch);
            Console.WriteLine("A batch of 3 events has been published.");
        }

        [Command("receive")]
        public async Task ReceiveAsync(string connectionString, string eventHubName, string blobConnectionString, string blobClobContainerName)
        {
            // Read from the default consumer group: $Default
            string consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

            // Create a blob container client that the event processor will use
            var storageClient = new BlobContainerClient(blobConnectionString, blobClobContainerName);

            // Create an event processor client to process events in the event hub
            var processor = new EventProcessorClient(storageClient, consumerGroup, connectionString, eventHubName);

            // Register handlers for processing events and handling errors
            processor.ProcessEventAsync += ProcessEventAsync;
            processor.ProcessErrorAsync += ProcessErrorAsync;

            // Start the processing
            await processor.StartProcessingAsync();

            await WaitForCtrlCAsync();

            // Stop the processing
            await processor.StopProcessingAsync();
        }

        public Task WaitForCtrlCAsync()
        {
            var stopCts = new TaskCompletionSource<object>();
            Console.CancelKeyPress += (s, e) => {
                e.Cancel = true;
                stopCts.SetResult(null);
            };
            return stopCts.Task;
        }

        private static async Task ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            await Console.Out.WriteLineAsync($"Partition {arg.PartitionId}: an unhandled exception was encountered. This was not expected to happen.");
            await Console.Out.WriteLineAsync(arg.Exception.ToString());
        }

        private async Task ProcessEventAsync(ProcessEventArgs arg)
        {
            // Write the body of the event to the console window
            await Console.Out.WriteLineAsync(Encoding.UTF8.GetString(arg.Data.Body.Span));

            // Update checkpoint in the blob storage so that the app receives only new events the next time it's run
            await arg.UpdateCheckpointAsync(arg.CancellationToken);
        }
    }
}
