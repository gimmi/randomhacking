using System;
using System.Threading;
using System.Threading.Tasks;
using Cocona;

namespace SimpleQueue
{
    public class Shared
    {
        private readonly int _messageSize;
        private readonly int _queueSize;
        private readonly byte[] _queue;

        private int _insertPtr;
        private int _removePtr;

        public Shared(int messageSize, int queueCapacity)
        {
            _messageSize = messageSize;
            _queueSize = messageSize * queueCapacity;
            _queue = new byte[_queueSize];
        }

        public int MessageSize => _messageSize;

        public int QueueSize => _queueSize;

        public int ReadInsertPtr() => _insertPtr;

        public void WriteInsertPtr(int insertPtr) => _insertPtr = insertPtr;

        public int ReadRemovePtr() => _removePtr;

        public void WriteRemovePtr(int removePtr) => _removePtr = removePtr;

        public byte[] ReadQueue(int startIndex, int endIndex)
        {
            var bytes = new byte[endIndex - startIndex];
            Array.Copy(_queue, startIndex, bytes, 0, bytes.Length);
            return bytes;
        }

        public void WriteQueue(int startIndex, byte[] bytes)
        {
            Array.Copy(bytes, 0, _queue, startIndex, bytes.Length);
        }
    }

    public class Producer
    {
        private readonly Shared _shared;
        private int _insertPtr;

        public Producer(Shared shared)
        {
            _shared = shared;
            _insertPtr = _shared.ReadInsertPtr();
        }

        public bool EnqueueMessage(byte[] message)
        {
            if (message.Length != _shared.MessageSize)
            {
                return false;
            }

            var removePtr = _shared.ReadRemovePtr();

            var newInsertPtr = _insertPtr + _shared.MessageSize;
            if (newInsertPtr == _shared.QueueSize)
            {
                newInsertPtr = 0;
            }

            if (newInsertPtr == removePtr)
            {
                return false;
            }

            _shared.WriteQueue(_insertPtr, message);
            _insertPtr = newInsertPtr;
            _shared.WriteInsertPtr(_insertPtr);
            return true;
        }
    }

    public class Consumer
    {
        private readonly Shared _shared;
        private int _removePtr;

        public Consumer(Shared shared)
        {
            _shared = shared;
            _removePtr = _shared.ReadRemovePtr();
        }

        public byte[][] DequeueMessages()
        {
            var insertPtr = _shared.ReadInsertPtr();

            if (_removePtr < insertPtr)
            {
                var bytes = _shared.ReadQueue(_removePtr, insertPtr);
                _removePtr = insertPtr;
                _shared.WriteRemovePtr(_removePtr);

                return SplitMessages(bytes);
            }

            if (_removePtr > insertPtr)
            {
                var bytes = _shared.ReadQueue(_removePtr, _shared.QueueSize);
                _removePtr = 0;
                _shared.WriteRemovePtr(_removePtr);
                return SplitMessages(bytes);
            }

            return new byte[0][];
        }

        private byte[][] SplitMessages(byte[] bytes)
        {
            var messageCount = bytes.Length / _shared.MessageSize;
            var messages = new byte[messageCount][];
            for (var i = 0; i < messageCount; i++)
            {
                var message = new byte[_shared.MessageSize];
                Array.Copy(bytes, i * _shared.MessageSize, message, 0, message.Length);
                messages[i] = message;
            }

            return messages;
        }
    }

    public class Program
    {
        public static void Main(string[] args) => CoconaApp.Run<Program>(args);

        public async Task MainAsync(int queueCapacity = 20, int produceRateMs = 100, int consumeRateMs = 1_000)
        {
            Console.Out.WriteLine("queueCapacity = {0}", queueCapacity);
            Console.Out.WriteLine("produceRateMs = {0}", produceRateMs);
            Console.Out.WriteLine("consumeRateMs = {0}", consumeRateMs);

            var ctrlC = BindCtrlC();

            var shared = new Shared(sizeof(int), queueCapacity);
            var producer = new Producer(shared);
            var consumer = new Consumer(shared);

            var producerTask = ProduceLoopAsync(produceRateMs, producer, ctrlC);
            var consumerTask = ConsumerLoopAsync(consumeRateMs, consumer, ctrlC);

            await Task.WhenAll(producerTask, consumerTask);
        }

        public static async Task ProduceLoopAsync(int produceRateMs, Producer producer, CancellationToken ct)
        {
            var counter = 0;
            while (await DelayAsync(produceRateMs, ct))
            {
                counter += 1;
                var message = BitConverter.GetBytes(counter);
                if (producer.EnqueueMessage(message))
                {
                    await Console.Out.WriteLineAsync("Sent: " + counter);
                }
                else
                {
                    await Console.Out.WriteLineAsync("QUQUE FULL! lost " + counter);
                }
            }
        }

        public static async Task ConsumerLoopAsync(int consumeRateMs, Consumer consumer, CancellationToken ct)
        {
            while (await DelayAsync(consumeRateMs, ct))
            {
                var messages = consumer.DequeueMessages();
                foreach (var message in messages)
                {
                    var counter = BitConverter.ToInt32(message);
                    await Console.Out.WriteLineAsync("Received: " + counter);
                }
            }
        }

        public static CancellationToken BindCtrlC()
        {
            var stopCts = new CancellationTokenSource();
            Console.CancelKeyPress += (s, e) => {
                e.Cancel = true;
                stopCts.Cancel();
            };
            return stopCts.Token;
        }

        public static async Task<bool> DelayAsync(int ms, CancellationToken ct)
        {
            try
            {
                await Task.Delay(ms, ct);
                return true;
            }
            catch (OperationCanceledException)
            {
                return false;
            }
        }
    }


}
