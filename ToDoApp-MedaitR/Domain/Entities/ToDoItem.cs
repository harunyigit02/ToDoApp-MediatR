using System.Text.Json.Serialization;

namespace ToDoApp_MedaitR.Domain.Entities
{
    public class ToDoItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
        public Guid UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
