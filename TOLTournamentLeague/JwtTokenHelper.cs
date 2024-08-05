using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtTokenHelper
{
    private readonly string _secretKey;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expirationInMinutes;

    public JwtTokenHelper(IConfiguration configuration)
    {
        _secretKey = configuration["JwtSettings:SecretKey"] ?? throw new ArgumentNullException("JwtSettings:SecretKey");
        _issuer = configuration["JwtSettings:Issuer"] ?? throw new ArgumentNullException("JwtSettings:Issuer");
        _audience = configuration["JwtSettings:Audience"] ?? throw new ArgumentNullException("JwtSettings:Audience");
        _expirationInMinutes = int.Parse(configuration["JwtSettings:ExpirationInMinutes"] ?? "60");
    }

    public string GenerateToken(string username)
    {
        if (string.IsNullOrEmpty(username))
            throw new ArgumentNullException(nameof(username));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            _issuer,
            _audience,
            claims,
            expires: DateTime.Now.AddMinutes(_expirationInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
