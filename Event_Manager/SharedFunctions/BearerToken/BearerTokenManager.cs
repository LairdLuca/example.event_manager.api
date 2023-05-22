using Event_Manager.BearerToken;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Event_Manager.SharedFunctions.BearerToken
{
    public static class BearerTokenManager
    {
        public const int DaysExpiresTime = 31;

        public static string RegisterUserToken(string guid, string email, bool isAdmin)
        {


            var secretKey = GetJWTSecret();
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JWTAuthorizedController._ID, guid));
            claims.Add(new Claim(JWTAuthorizedController._EMAIL, email));
            claims.Add(new Claim(JWTAuthorizedController._ADMIN, isAdmin.ToString()));

            var tokeOptions = new JwtSecurityToken(issuer: Program.Configuration["JWT:ValidIssuer"], audience: Program.Configuration["JWT:ValidAudience"], claims, expires: DateTime.Now.AddDays(DaysExpiresTime), signingCredentials: signinCredentials);
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return tokenString;
        }

        public static SymmetricSecurityKey GetJWTSecret()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Program.Configuration["JWT:Secret"]
                        ?? throw new Exception("JWT not configureted correctly")));

        }
    }
}
