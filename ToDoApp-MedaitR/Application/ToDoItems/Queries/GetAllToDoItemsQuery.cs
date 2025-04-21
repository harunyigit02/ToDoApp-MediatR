using MediatR;
using ToDoApp_MedaitR.Domain.Entities;

namespace ToDoApp_MedaitR.Application.ToDoItems.Queries
{
    public sealed record GetAllToDoItemsQuery : IRequest<List<ToDoItem>> { };
}
