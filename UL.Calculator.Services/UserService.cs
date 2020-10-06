using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UL.Calculator.Core.Model;
using UL.Calculator.Data;
using UL.Calculator.Entities;

namespace UL.Calculator.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IRepository<UserLogin> _repository;

        public UserService(IConfiguration config, IMapper mapper, IRepository<UserLogin> repository)
        {
            _config = config;
            _mapper = mapper;
            _repository = repository;
        }

        public Task<UserInfo> Authenticate(Credentials credentials)
        {
            var userLogin = VerifyAndGetUserDetails(credentials);

            // return null if user not found
            if (userLogin == null)
                return Task.FromResult<UserInfo>(null);

            userLogin.Token = GenerateToken(userLogin);
            UserInfo userInfo = _mapper.Map<UserInfo>(userLogin);

            return Task.FromResult(userInfo);
        }

        private UserLogin VerifyAndGetUserDetails(Credentials credentials)
        {
            return _repository.FindBy(user => user.Username == credentials.Username && user.Password == credentials.Password,
                                      x => x.User).SingleOrDefault();
        }

        private string GenerateToken(UserLogin userLogin)
        {
            // authentication successful hence generating JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var appSecret = _config.GetValue<string>("AppSettings:Secret");
            var key = Encoding.ASCII.GetBytes(appSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, $"{userLogin.User.FirstName} {userLogin.User.LastName}"),
                    new Claim(ClaimTypes.Role, userLogin.SubscriptionType.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, userLogin.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}