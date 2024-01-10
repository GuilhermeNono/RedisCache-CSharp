﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RedisCachingProject.Abstraction;
using RedisCachingProject.Database.Model;

namespace RedisCachingProject.Authentication;

public class JwtProvider : IJwtProvider
{

    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public string Generate(Person person)
    {

        var claims = new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, person.Id.ToString()),
            new(JwtRegisteredClaimNames.Name, person.FirstName)
        };

        var signingCredentials = _options.SigningCredentials;
        
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signingCredentials);

        var tokenValue = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return tokenValue;
    }
}