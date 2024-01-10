using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RedisCachingProject.Authentication;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SecretKey { get; set; }
    public SigningCredentials SigningCredentials => 
        new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)), SecurityAlgorithms.HmacSha256);
    public SecurityKey AccessKey => SigningCredentials.Key;
}