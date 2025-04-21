using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp_MedaitR.Application.ToDoItems.Queries;
using ToDoApp_MedaitR.Domain.Entities;
using ToDoApp_MedaitR.Infrastructure.Persistence;

namespace ToDoApp_MedaitR.Application.ToDoItems.Handlers
{
    public class GetAllToDoItemsHandler : IRequestHandler<GetAllToDoItemsQuery, List<ToDoItem>>
    {
        private readonly AppDbContext _context;

        public GetAllToDoItemsHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDoItem>> Handle(GetAllToDoItemsQuery request, CancellationToken cancellationToken)
        {
            return await _context.ToDoItems.ToListAsync(cancellationToken);
        }
    }
}
