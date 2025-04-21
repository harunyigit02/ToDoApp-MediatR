using MediatR;

namespace ToDoApp_MedaitR.Application.Auth.Commands
{
    public class RegisterCommand : IRequest<Unit>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
