using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using ToDoApp_MedaitR.Application.Auth.Commands;
using ToDoApp_MedaitR.Domain.Entities;
using ToDoApp_MedaitR.Infrastructure.Persistence;

namespace ToDoApp_MedaitR.Application.Auth.Handlers
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, Unit>
    {
        private readonly AppDbContext _context;

        public RegisterHandler(AppDbContext context)
        {
            _context = context;
        }

        // Handle metodunun dönüş tipi Task<Unit> olacak şekilde güncellenmeli
        public async Task<Unit> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (await _context.Users.AnyAsync(u => u.UserName == request.UserName))
                throw new Exception("Bu kullanıcı adı zaten mevcut.");

            using var hmac = new HMACSHA512();
            var user = new User
            {
                UserName = request.UserName,
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;  // İşlem başarılı olduğunda Unit.Value döndürüyoruz
        }
    }
}
