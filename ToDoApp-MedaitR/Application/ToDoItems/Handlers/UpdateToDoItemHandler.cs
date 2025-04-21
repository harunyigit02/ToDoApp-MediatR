using MediatR;
using ToDoApp_MedaitR.Application.ToDoItems.Commands;
using ToDoApp_MedaitR.Domain.Entities;
using ToDoApp_MedaitR.Infrastructure.Persistence;

namespace ToDoApp_MedaitR.Application.ToDoItems.Handlers
{
    public class UpdateToDoItemHandler : IRequestHandler<UpdateToDoItemCommand, ToDoItem>
    {
        private readonly AppDbContext _context;

        public UpdateToDoItemHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ToDoItem> Handle(UpdateToDoItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.ToDoItems.FindAsync(request.Id);
            if (item == null)
                return null;

            item.Description = request.Description;
            item.Date = request.Date;
            item.IsCompleted = request.IsCompleted;

            await _context.SaveChangesAsync(cancellationToken);

            return item;
        }
    }
}
