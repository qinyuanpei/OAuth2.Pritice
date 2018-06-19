using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OAuth2.Pritice.JwtAuthorization
{
    public class JwtTokenManager
    {
        /// <summary>
        /// 对称密钥
        /// 产生方式：Convert.ToBase64String(new HMACSHA256().Key)
        /// </summary>
        private const string secretKey = "db3OIsj+BXE9NZDy0t8W3TcNekrF+2d/1sFnWG4HnV8TZY30iTOdtVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

        /// <summary>
        /// 过期时间，默认为30分钟
        /// </summary>
        private const int expireMinutes = 30;

        /// <summary>
        /// 生成令牌
        /// </summary>
        /// <param name="clientId">用户名</param>
        /// <param name="clientSecret">密码(可选)</param>
        /// <returns></returns>
        public static string GenerateToken(string clientId, string clientSecret = "")
        {
            var symmetricKey = Convert.FromBase64String(secretKey);
            var tokenHandler = new JwtSecurityTokenHandler();

            var now = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, clientId),
                    new Claim("host",Dns.GetHostName()),
                    new Claim("lover","何晓")
                }),

                Expires = now.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256Signature)
            };

            var stoken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(stoken);

            return token;
        }

        /// <summary>
        /// 验证令牌
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public static ClaimsPrincipal ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null) return null;

                var symmetricKey = Convert.FromBase64String(secretKey);

                var validationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                };

                SecurityToken securityToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

                return principal;
            }

            catch (Exception)
            {
                return null;
            }
        }
    }
}
