namespace CodeInTasks.Shared.Queues.Abstractions.Models
{
    public sealed class Message<TData>
    {
        public string Id { get; set; }
        public TData Data { get; set; }
    }
}
