using CodeInTasks.Shared.Wrappers.Interfaces;
using StackExchange.Redis;

namespace CodeInTasks.Shared.Queues
{
    internal class MessageQueue<TMessageData> : IMessageQueue<TMessageData>
    {
        private readonly IDatabase redisDb;
        private readonly IJsonSerializer serializer;

        private readonly string QueueKey;

        public MessageQueue(IDatabase redisDb, IJsonSerializer serializer)
        {
            this.redisDb = redisDb;
            this.serializer = serializer;

            QueueKey = typeof(TMessageData).Name;
        }

        public Task AcknowledgeAsync(Message<TMessageData> message)
        {
            return redisDb.StreamAcknowledgeAsync(QueueKey, QueueConstants.GroupName, message.Id, CommandFlags.FireAndForget);
        }

        public async Task<Message<TMessageData>> GetMessageAsync(string consumerName)
        {
            var groupName = QueueConstants.GroupName;

            await EnsureGroupCreated(groupName);

            var rawMessages = await redisDb.StreamReadGroupAsync(QueueKey, groupName, consumerName, count: 1);

            if (rawMessages.Length == 0)
            {
                return null;
            }
            else
            {
                var rawMessage = rawMessages[0];

                var dataString = rawMessage[QueueConstants.FieldName];
                var data = serializer.Deserialize<TMessageData>(dataString);

                var messageId = rawMessage.Id;

                return new(messageId, data);
            }
        }

        public Task PublishAsync(TMessageData data)
        {
            var messageData = serializer.Serialize(data);

            return redisDb.StreamAddAsync(
                QueueKey, QueueConstants.FieldName, messageData,
                maxLength: QueueConstants.MaxQueueLength,
                useApproximateMaxLength: true,
                flags: CommandFlags.FireAndForget);
        }

        private Task EnsureGroupCreated(string groupName)
        {
            return redisDb.StreamCreateConsumerGroupAsync(QueueKey, groupName, StreamPosition.NewMessages, CommandFlags.FireAndForget);
        }
    }
}
