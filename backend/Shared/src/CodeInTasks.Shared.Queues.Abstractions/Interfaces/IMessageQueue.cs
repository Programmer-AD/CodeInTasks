namespace CodeInTasks.Shared.Queues.Abstractions.Interfaces
{
    public interface IMessageQueue<TMessageData>
    {
        Task PublishAsync(TMessageData data);
        Task<Message<TMessageData>> GetMessageAsync(string consumerName);
        Task AcknowledgeAsync(Message<TMessageData> message);
    }
}
