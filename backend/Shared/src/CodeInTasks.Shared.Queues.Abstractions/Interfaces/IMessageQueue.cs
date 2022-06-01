namespace CodeInTasks.Shared.Queues.Abstractions.Interfaces
{
    public interface IMessageQueue<TMessageData>
    {
        Task PublishAsync(TMessageData data);
        Task<Message<TMessageData>> GetMessageAsync();
        Task AcknowledgeAsync(Message<TMessageData> message);
    }
}
