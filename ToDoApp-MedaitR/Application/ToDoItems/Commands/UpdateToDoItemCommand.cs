using MediatR;
using ToDoApp_MedaitR.Domain.Entities;

namespace ToDoApp_MedaitR.Application.ToDoItems.Commands
{
    public class UpdateToDoItemCommand : IRequest<ToDoItem>
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public bool IsCompleted { get; set; }
    }
}
