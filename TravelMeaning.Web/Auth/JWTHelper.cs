using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TravelMeaning.Common.Helper;

namespace TravelMeaning.Web.Auth
{
    public class JWTHelper
    {
        public static string IssueJWT(CustomPayloadModel model)
        {
            DateTime dateTime = DateTime.Now;
            string iss = Appsettings.app(new string[] { "Audience", "Issuer" });
            string aud = Appsettings.app(new string[] { "Audience", "Audience" });
            string secret = Appsettings.app(new string[] { "Audience", "Secret" });
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,model.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,(new DateTimeOffset(dateTime).ToUnixTimeSeconds()).ToString()),
                new Claim(JwtRegisteredClaimNames.Nbf,(new DateTimeOffset(dateTime).ToUnixTimeSeconds()).ToString()),
                new Claim(JwtRegisteredClaimNames.Exp,(new DateTimeOffset(dateTime).AddSeconds(1800).ToUnixTimeSeconds()).ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(1800).ToString()),
                new Claim(JwtRegisteredClaimNames.Iss,iss),
                new Claim(JwtRegisteredClaimNames.Aud,aud),
            };
            claims.AddRange(model.Role.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: iss,
                claims: claims,
                signingCredentials: creds
                );
            var jwtHandler = new JwtSecurityTokenHandler();
            var encoderdJWT = jwtHandler.WriteToken(jwt);
            return encoderdJWT;
        }
        public static CustomPayloadModel SeriallzeJwt(string jwtStr)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(jwtStr);
            object role = new object();
            Guid Id;
            Guid.TryParse(jwtToken.Id, out Id);
            try
            {
                jwtToken.Payload.TryGetValue(ClaimTypes.Role, out role);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var model = new CustomPayloadModel
            {
                Id = Id,
                Role = role != null ? role.ToString() : string.Empty,
            };
            return model;
        }
    }
}
