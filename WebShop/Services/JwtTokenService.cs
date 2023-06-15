using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebShop.Abstract;
using WebShop.Data.Entities.Identity;

namespace WebShop.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<UserEntity> _userManeger;

        public JwtTokenService(IConfiguration config, UserManager<UserEntity> userManeger)
        {
            _config = config;
            _userManeger = userManeger;
        }

        public async Task<string> CreateToken(UserEntity user)
        {
            var roles = await _userManeger.GetRolesAsync(user);
            List<Claim> claims = new List<Claim>()
            {
                new Claim("email", user.Email),
                new Claim("image", user.Image ?? string.Empty),
            };
            foreach(var role in roles)
            {
                claims.Add(new Claim("roles", role));

            }
            var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<String>("JwtSecretKey")));
            var singInCredentials = new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                signingCredentials: singInCredentials,
                expires: DateTime.Now.AddDays(10),
                claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
