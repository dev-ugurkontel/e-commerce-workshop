using Core.Entity.Model;
using Microsoft.Extensions.Configuration;
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
        public IConfiguration Configuration { get; }
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
            var jwt = CreateSecurityToken(_tokenOptions,user);
        }

        public JwtSecurityToken CreateSecurityToken(TokenOptions options,UserTokenModel user)
        {
            var jwt = new JwtSecurityToken(
                    issuer:options.Issuer,
                    audience:options.Audience,
                    expires:_accessTokenExpiration,
                    claims: SetClaims(user),
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
