using MediatR;
using ToDoApp_MedaitR.Domain.Entities;

namespace ToDoApp_MedaitR.Application.ToDoItems.Queries
{
    public class GetToDoItemsByDateQuery: IRequest<List<ToDoItem>>

    {
        public DateTime Date { get; set; }

        public GetToDoItemsByDateQuery(DateTime date) => Date = date;
    }
}
