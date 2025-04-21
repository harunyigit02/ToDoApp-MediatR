using MediatR;
using Microsoft.EntityFrameworkCore;
using ToDoApp_MedaitR.Application.Auth.Commands;
using ToDoApp_MedaitR.Application.Auth.Dto;
using ToDoApp_MedaitR.Infrastructure.Authentication;
using ToDoApp_MedaitR.Infrastructure.Persistence;

namespace ToDoApp_MedaitR.Application.Auth.Handlers
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, LoginResponse>
    {
        private readonly AppDbContext _context; // Kullanıcıyı sorgulamak için veri tabanına erişim
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public RefreshTokenHandler(AppDbContext context, JwtTokenGenerator jwtTokenGenerator)
        {
            _context = context;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<LoginResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            // Veritabanında Refresh Token'ı arıyoruz
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.RefreshToken == request.RefreshToken, cancellationToken);

            if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
            {
                // Eğer kullanıcı yoksa ya da Refresh Token süresi dolmuşsa
                throw new UnauthorizedAccessException("Refresh Token geçersiz veya süresi dolmuş.");
            }

            // Yeni Access Token oluşturuyoruz
            var accessToken = _jwtTokenGenerator.GenerateAccessToken(user.Id, user.Role);

            // Refresh Token'ı değiştirmiyoruz çünkü sadece login sırasında yenilenir
            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = user.RefreshToken // Eski Refresh Token'ı döndürüyoruz
            };
        }
    }
}
