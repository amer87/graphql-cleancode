using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Com.Application.Common.Interfaces;

public interface IJwtProvider
{
    string GetToken(Guid sid, string name, string role, Guid? fid, string secrete);
}

public class JwtProvider : IJwtProvider
{
    public string GetToken(Guid sid, string name, string role, Guid? fid, string secrete)
    {
        // authentication successful so generate jwt token
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secrete);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Sid, sid.ToString()),
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role),
                new Claim("fid", fid.GetValueOrDefault().ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var stoken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(stoken);
    }
}
