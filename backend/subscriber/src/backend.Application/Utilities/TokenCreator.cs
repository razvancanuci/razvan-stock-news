using System.IdentityModel.Tokens.Jwt;

namespace backend.Application.Utilities
{
    public class TokenCreator
    {
        public static string WriteToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityClaims = tokenHandler.ReadJwtToken(token);
            var jwt = tokenHandler.WriteToken(securityClaims);

            return jwt;
        }
    }
}
