using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp_MedaitR.Application.ToDoItems.Queries;
using ToDoApp_MedaitR.Domain.Entities;
using ToDoApp_MedaitR.Infrastructure.Persistence;

namespace ToDoApp_MedaitR.Application.ToDoItems.Handlers
{
    public class GetToDoItemsByDateHandler : IRequestHandler<GetToDoItemsByDateQuery, List<ToDoItem>>
    {
        private readonly AppDbContext _context;
        public GetToDoItemsByDateHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ToDoItem>> Handle(GetToDoItemsByDateQuery request, CancellationToken cancellationToken)
        {
            var filteredItems = await _context.ToDoItems
                .Where(x => x.Date == request.Date)
                .ToListAsync();
            return filteredItems;
        }

    }
}
