using MediatR;
using ToDoApp_MedaitR.Application.Auth.Dto;

namespace ToDoApp_MedaitR.Application.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<LoginResponse>
    {
        public string RefreshToken { get; set; }
    }
}
