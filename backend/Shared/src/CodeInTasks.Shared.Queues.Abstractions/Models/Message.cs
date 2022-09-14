namespace CodeInTasks.Shared.Queues.Abstractions.Models
{
    public sealed class Message<TData>
    {
        public Message(string id, TData data)
        {
            Id = id;
            Data = data;
        }

        public string Id { get; set; }
        public TData Data { get; set; }
    }
}
