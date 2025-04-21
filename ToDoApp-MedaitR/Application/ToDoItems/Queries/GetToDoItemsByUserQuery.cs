using MediatR;
using ToDoApp_MedaitR.Domain.Entities;

namespace ToDoApp_MedaitR.Application.ToDoItems.Queries
{
    public class GetToDoItemsByUserQuery : IRequest<List<ToDoItem>>
    {
        public Guid Id { get; set; }
        public GetToDoItemsByUserQuery(Guid id)
        {
            Id = id;
        }
    }
}
