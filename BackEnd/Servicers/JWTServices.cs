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
            // get jwt key from enviroment variables
            var env = new EnviromentVariables();
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(env.GetJwtKey());
            // create claims from the token handler and add them to the token handler
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UID),
                    //email bonus - unique email of user
                    new Claim(ClaimTypes.Email, user.Email)
            };
            // add roles to the claims
            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            // create the token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                Audience = "http://localhost:5144",
                Issuer = "http://localhost:5144",
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            // create the token
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
