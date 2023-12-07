namespace Place.Api.Infrastructure.Persistence.Authentication.EF.Models;

using System;
using Domain.Common.Abstractions;

internal class UserReadModel : IAuditableEntity, ISoftDeletableEntity
{
    public Ulid Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public bool EmailIsConfirmed { get; set; }
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedOnUtc { get; set; }
    public DateTime? ModifiedOnUtc { get; set; }
    public DateTime? DeletedOnUtc { get; set; }
    public bool Deleted { get; set; }
}
