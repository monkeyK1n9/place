namespace Place.Api.Infrastructure.Authentication;

using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Services;
using Domain.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

/// <summary>
/// Provides functionality to generate JSON Web Token (JWT) for authentication purposes.
/// </summary>
/// <remarks>
/// This class is responsible for creating JWTs based on the provided user information and JWT settings.
/// It utilizes the System.IdentityModel.Tokens.Jwt library to construct and encode the token.
/// </remarks>
public class JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions)
    : IJwTokenGenerator
{
    private readonly JwtSettings jwtSettings = jwtOptions.Value;

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
        SigningCredentials signingCredentials = new(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(this.jwtSettings.Secret)),
            SecurityAlgorithms.HmacSha256
        );

        Debug.Assert(user != null!, nameof(user) + " != null");
        Claim[] claims = {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()!),
            new(JwtRegisteredClaimNames.GivenName, user.UserName.Value),
            new(JwtRegisteredClaimNames.Email, user.Email.Value),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken securityToken = new(
            issuer: this.jwtSettings.Issuer,
            audience: this.jwtSettings.Audience,
            expires: dateTimeProvider.UtcNow.AddMinutes(this.jwtSettings.ExpiryInMinutes),
            claims: claims,
            signingCredentials: signingCredentials
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
