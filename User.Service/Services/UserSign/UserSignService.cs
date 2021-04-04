using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Enitites;
using User.Domain.Interfaces;
using User.Service.Configs;
using User.Service.Models.User;

namespace User.Service.Services.UserSign
{
    public class UserSignService : IUserSignService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly SecretKeyConfig _secretKeyConfig;

        public UserSignService(IMapper mapper, IUserService userService, IOptions<SecretKeyConfig> secretKeyConfig)
        {
            _mapper = mapper;
            _userService = userService;
            _secretKeyConfig = secretKeyConfig.Value;
        }

        public async Task<string> GenerateToken(LoginModel loginModel)
        {
            var login = _mapper.Map<Login>(loginModel);

            var user = await _userService.Login(login);

            var claims = new List<Claim> { 
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKeyConfig.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = Authorization.ISSUER,
                Audience = Authorization.AUDIENCE,

                SigningCredentials = credentials,

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(4),
                NotBefore = DateTime.UtcNow
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
