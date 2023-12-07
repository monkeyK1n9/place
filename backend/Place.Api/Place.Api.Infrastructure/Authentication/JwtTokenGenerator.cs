namespace Place.Api.Infrastructure.Authentication;

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Domain.Authentication;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

/// <summary>
/// Provides functionality to generate JSON Web Token (JWT) for authentication purposes.
/// </summary>
/// <remarks>
/// This class is responsible for creating JWTs based on the provided user information and JWT settings.
/// It utilizes the System.IdentityModel.Tokens.Jwt library to construct and encode the token.
/// </remarks>
public class JwtTokenGenerator(IDateTimeProvider dateTimeProvider, JwtSettings jwtSettings)
    : IJwTokenGenerator
{
    /// <summary>
    /// Generates a JWT for the specified user.
    /// </summary>
    /// <param name="user">The user for whom the token is being generated.</param>
    /// <returns>A string representing the encoded JWT.</returns>
    /// <remarks>
    /// The token includes standard claims such as subject, given name, email, and a unique identifier.
    /// The token's expiration is set based on the configured expiry time in minutes.
    /// </remarks>
    public string GenerateToken(User user)
    {
        SigningCredentials signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        Claim[] claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()!),
            new Claim(JwtRegisteredClaimNames.GivenName, user.UserName.Value),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken securityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            expires: dateTimeProvider.UtcNow.AddMinutes(jwtSettings.ExpiryInMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
