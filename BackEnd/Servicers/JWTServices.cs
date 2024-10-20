using BackEnd.Configs.Enviroments;
using BackEnd.Database.Tables;
using BackEnd.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Servicers
{
    public class JWTServices
    {
        public static string GenerateJwtToken(UserDTO user)
        {
            var env = new EnviromentVariables();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(env.GetJwtKey());
            //new Claim[]
            //    {
            //        // Lấy UID từ token
            //        //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //    }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UID),
                    //email bonus - unique email of user
                    new Claim(ClaimTypes.Email, user.Email)
            };
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Audience = "http://localhost:5144",
                Issuer = "http://localhost:5144",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
