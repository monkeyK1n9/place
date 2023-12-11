namespace Place.Api.Infrastructure.Persistence.EF.Authentication.Repositories;

using System.Threading.Tasks;
using Contexts;
using Microsoft.EntityFrameworkCore;
using Place.Api.Application.Common.Interfaces.Authentication;
using Place.Api.Domain.Authentication;
using Place.Api.Domain.Authentication.ValueObjects;

public class UserRepository(WriteDbContext context) : IUserRepository
{
    private readonly DbSet<User> users = context.Users;

    public async Task<bool> IsUniqueEmail(Email email) =>
        await this.users.AnyAsync(user => user.Email == email)
            .ConfigureAwait(false);

    public async Task AddAsync(User user)
    {
        await this.users.AddAsync(user).ConfigureAwait(true);
        await context
            .SaveChangesAsync()
            .ConfigureAwait(true);
    }
}
