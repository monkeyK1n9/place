namespace Place.Api.Application.Common.Interfaces.Authentication;

using System.Threading.Tasks;
using Domain.Authentication;
using Domain.Authentication.ValueObjects;

public interface IUserRepository
{
    Task<bool> IsUniqueEmail(Email email);
    Task AddAsync(User user);
}
