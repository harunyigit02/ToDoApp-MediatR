using MediatR;
using ToDoApp_MedaitR.Application.ToDoItems.Commands;
using ToDoApp_MedaitR.Domain.Entities;
using ToDoApp_MedaitR.Infrastructure.Persistence;

namespace ToDoApp_MedaitR.Application.ToDoItems.Handlers
{
     public class AddToDoItemHandler : IRequestHandler<AddToDoItemCommand, ToDoItem>
    {
        private readonly AppDbContext _context;

        public AddToDoItemHandler(AppDbContext context)
        {
            _context = context; 
        }

        
        public async Task<ToDoItem> Handle(AddToDoItemCommand request, CancellationToken cancellationToken)
        {
            var todoItem = new ToDoItem
            {
                Description = request.Description,
                Date = request.Date,
                
            };
            _context.ToDoItems.Add(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }
    }
}
