using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using User.Domain.DTO;
using User.Domain.Interfaces;
using User.Service.Configs;

namespace User.Service.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IUserService _userService;
        private readonly SecretKeyConfig _secretKeyConfig;

        public TokenService(IUserService userService, IOptions<SecretKeyConfig> secretKeyConfig)
        {
            _userService = userService;
            _secretKeyConfig = secretKeyConfig.Value;
        }

        public string GenerateToken(LoginDto login)
        {
            var user = _userService.Login(login);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("name", user.Name)
            };

            var identity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            var jwt = new JwtSecurityToken(
                   issuer: Authorization.ISSUER,
                   audience: Authorization.AUDIENCE,
                   notBefore: DateTime.UtcNow,
                   claims: identity.Claims,
                   expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                   signingCredentials: Authorization.GenerateSigningCredentials(_secretKeyConfig.Key));
            
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }
    }
}
