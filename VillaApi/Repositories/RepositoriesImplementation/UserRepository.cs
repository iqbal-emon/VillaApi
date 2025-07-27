using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VillaApi.Data;
using VillaApi.Model.Dtos.LoginDto;
using VillaApi.Repositories.IRepositories;
using VillaApi.Utility;

namespace VillaApi.Repositories.RepositoriesImplementation
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            _configuration = configuration;
        }

        public async Task<ApiResponse> Login(LoginRequestDto loginRequestDto)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Name.ToLower() == loginRequestDto.Name.ToLower() && u.Password == loginRequestDto.Password);
            if (user == null)
            {
                return new ApiResponse
                {
                    Success = false,
                    Message = "Invalid username or password",
                    StatusCode = 401
                };
            }

          var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("ApiSetting:SecretKey"));
            var tokenHandler=new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("name", user.Name),
                    new Claim("role", user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new ApiResponse
            {
                Success = true,
                Message = "Login successful",
                Data = new LoginResponseDto
                {
                    Token = tokenHandler.WriteToken(token),
                    user = user
                },
                StatusCode = 200
            };
        }

        
    }
}
