using MediatR;

namespace ToDoApp_MedaitR.Application.ToDoItems.Commands
{
    public class DeleteToDoItemCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DeleteToDoItemCommand(Guid id) { Id = id; }
    }
}
