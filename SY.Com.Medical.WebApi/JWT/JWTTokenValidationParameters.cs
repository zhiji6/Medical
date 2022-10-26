using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SY.Com.Medical.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SY.Com.Medical.WebApi.JWT
{
    /// <summary>
    /// 获取JWT的认证参数
    /// </summary>
    public class JWTTokenValidationParameters
    {
        /// <summary>
        /// 返回JWT参数
        /// </summary>
        /// <returns></returns>
        public static TokenValidationParameters getParameters()
        {
            //生成密钥
            var symmetricKeyAsBase64 = ReadConfig.GetConfigSection("Jwt:Secret");
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,//是否验证签名,不验证的画可以篡改数据，不安全
                IssuerSigningKey = signingKey,//解密的密钥
                ValidateIssuer = true,//是否验证发行人，就是验证载荷中的Iss是否对应ValidIssuer参数
                ValidIssuer = ReadConfig.GetConfigSection("Jwt:Iss"),//发行人
                ValidateAudience = true,//是否验证订阅人，就是验证载荷中的Aud是否对应ValidAudience参数
                ValidAudience = ReadConfig.GetConfigSection("Jwt:Aud"),//订阅人
                ValidateLifetime = true,//是否验证过期时间，过期了就拒绝访问
                ClockSkew = TimeSpan.Zero,//这个是缓冲过期时间，也就是说，即使我们配置了过期时间，这里也要考虑进去，过期时间+缓冲，默认好像是7分钟，你可以直接设置为0
                RequireExpirationTime = true,
            };
        }



        /// <summary>
        /// 根据用户的userid和account来返回JWT的token
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="Account"></param>
        /// <returns></returns>
        public static string getSecurityToken(int UserId,string Account)
        {            
            //秘钥，就是标头，这里用Hmacsha256算法，需要256bit的密钥
            var securityKey = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(ReadConfig.GetConfigSection("Jwt:Secret"))), SecurityAlgorithms.HmacSha256);
            //Claim，JwtRegisteredClaimNames中预定义了好多种默认的参数名，也可以像下面的Guid一样自己定义键名.
            //ClaimTypes也预定义了好多类型如role、email、name。Role用于赋予权限，不同的角色可以访问不同的接口
            //相当于有效载荷
            var claims = new Claim[] {
                new Claim(JwtRegisteredClaimNames.Iss,ReadConfig.GetConfigSection("Jwt:Iss")),
                new Claim(JwtRegisteredClaimNames.Aud,ReadConfig.GetConfigSection("Jwt:Aud")),
                new Claim("UserId",UserId.ToString()),
                new Claim("Account",Account)
            };
            SecurityToken securityToken = new JwtSecurityToken(
                signingCredentials: securityKey,
                expires: DateTime.Now.AddMinutes(120),//过期时间
                claims: claims
            );
            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
