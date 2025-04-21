using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp_MedaitR.Application.ToDoItems.Queries;
using ToDoApp_MedaitR.Domain.Entities;
using ToDoApp_MedaitR.Infrastructure.Persistence;

namespace ToDoApp_MedaitR.Application.ToDoItems.Handlers
{
    public class GetToDoItemsByUserHandler : IRequestHandler<GetToDoItemsByUserQuery, List<ToDoItem>>
    {
        private readonly AppDbContext _context;

        public GetToDoItemsByUserHandler(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<ToDoItem>> Handle(GetToDoItemsByUserQuery request, CancellationToken cancellationToken)
        {
            var todos = await _context.ToDoItems
                .Where(t => t.UserId == request.Id)
                .ToListAsync();
            return todos;
        }
    }
}
