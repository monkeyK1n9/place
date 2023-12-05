namespace Place.Api.Application.Common.Interfaces.Authentication;

using Domain.Authentication;

public interface IJwTokenGenerator
{
    string GenerateToken(User user);
}
