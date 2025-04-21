using MediatR;
using ToDoApp_MedaitR.Application.Auth.Dto;

namespace ToDoApp_MedaitR.Application.Auth.Commands
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
