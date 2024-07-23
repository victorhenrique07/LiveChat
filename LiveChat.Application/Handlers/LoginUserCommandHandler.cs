using LiveChat.Application.Commands;
using LiveChat.Domain.Models;
using LiveChat.Domain.Repository;
using LiveChat.Application.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LiveChat.Application.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginResponse>
    {
        private const string TokenSecret = "bXlhcHB2ZXJ5c3Ryb25nc2VjcmV0a2V5MTIzNDU2";

        private readonly IUserRepository userRepository;

        public LoginUserCommandHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<LoginResponse> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var currentUser = await userRepository.AuthenticateUserAsync(command.Email, command.Password);

            if (currentUser != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(TokenSecret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, currentUser.Email),
                        new Claim(ClaimTypes.Role, currentUser.Role),
                        new Claim(ClaimTypes.NameIdentifier, currentUser.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = "https://localhost:7130",
                    Audience = "https://localhost:7130",
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return new LoginResponse
                {
                    Token = tokenString,
                    Message = "Login Authorized!"
                };
            }

            return new LoginResponse{ Token = null, Message = "Login Unauthorized!"};
        }
    }
}
