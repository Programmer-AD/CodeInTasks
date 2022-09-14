namespace CodeInTasks.Domain.Models
{
    public class ModelBase
    {
        public Guid Id { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
