using MediatR;
using ToDoApp_MedaitR.Application.ToDoItems.Commands;
using ToDoApp_MedaitR.Infrastructure.Persistence;

namespace ToDoApp_MedaitR.Application.ToDoItems.Handlers
{
    public class DeleteToDoItemHandler : IRequestHandler<DeleteToDoItemCommand, bool>
    {
        private readonly AppDbContext _context;

        public DeleteToDoItemHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.ToDoItems.FindAsync(request.Id);
            if (item == null)
                return false;

            _context.ToDoItems.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
