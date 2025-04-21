using MediatR;
using ToDoApp_MedaitR.Domain.Entities;

namespace ToDoApp_MedaitR.Application.ToDoItems.Commands
{
    public class AddToDoItemCommand : IRequest<ToDoItem>
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
