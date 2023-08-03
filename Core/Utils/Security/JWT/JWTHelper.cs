using Azure.Identity;
using Core.Entity.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utils.Security.JWT
{
    public class JWTHelper : ITokenHelper
    {
        public IConfiguration Configuration { get;}
        private TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JWTHelper(IConfiguration configuration)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions") as TokenOptions;            
        }
        public AccessToken CreateToken(UserTokenModel user)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            var jwt = CreateJwtSecurityToken(_tokenOptions, user);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);

            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, UserTokenModel user )
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                claims : SetClaims(user),
                notBefore: DateTime.Now            
                
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(UserTokenModel user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("Name", user.UserFirstName));
            claims.Add(new Claim("LastName", user.UserLastName));
            claims.Add(new Claim("Email", user.UserEmail));           
           

            return claims;
        }
    }
}
