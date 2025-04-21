using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using ToDoApp_MedaitR.Application.Auth.Commands;
using ToDoApp_MedaitR.Application.Auth.Dto;
using ToDoApp_MedaitR.Infrastructure.Authentication;
using ToDoApp_MedaitR.Infrastructure.Persistence;

namespace ToDoApp_MedaitR.Application.Auth.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly AppDbContext _context; // Kullanıcıyı sorgulamak için veri tabanına erişim

        public LoginHandler(JwtTokenGenerator jwtTokenGenerator, AppDbContext context)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            // Kullanıcıyı veritabanından al
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Kullanıcı bulunamadı.");
            }

            // Şifreyi doğrula
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
            if (!computedHash.SequenceEqual(user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Kullanıcı adı veya Şifre yanlış.");
            }

            // Refresh Token oluştur (eğer mevcut değilse)
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);  // Refresh token'ın geçerliliği 7 gün

            await _context.SaveChangesAsync();

            // JWT Token'ı oluştur
            var accessToken = _jwtTokenGenerator.GenerateAccessToken(user.Id, user.Role);

            // Access token ve refresh token döndür
            return new LoginResponse { AccessToken = accessToken, RefreshToken = refreshToken };
        }


    }
}
