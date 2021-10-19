using AdopteUnDevApi.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdopteUnDevApi.Tools
{
    public class TokenManager : ITokenManager
    {
        public static readonly string _secretKey = "Les mangues mûres du mangier sont dans mon frigo";
        public static readonly string _issuer = "mysite.com"; // appli qui consomme l'API
        public static string _audience = "myapidomain"; // Nom de domaine du site qui héberge l'API

        public UserModel GenerateJWT(UserModel user)
        {
            // Création de la clé de validation
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            // Création de l'objet de sécurité qui contiendra les informations de l'utilisateur à stockée
            Claim[] claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("Telephone", user.Telephone),
                new Claim(ClaimTypes.Role, (user.IsClient ? "client" : "developper"))
            };

            //génération du token (package System.IdentityModel.Tokens.Jwt)
            JwtSecurityToken token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials,
                    issuer: _issuer,
                    audience: _audience
                );

            // Hasher toutes les informations pour générer une chaine de caractères
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string completeToken = handler.WriteToken(token);

            user.Token = completeToken;
            return user;
        }
    }
}
