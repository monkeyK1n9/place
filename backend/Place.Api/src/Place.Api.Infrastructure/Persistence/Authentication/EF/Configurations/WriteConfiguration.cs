namespace Place.Api.Infrastructure.Persistence.Authentication.EF.Configurations;

using Constants;
using Domain.Authentication;
using Domain.Authentication.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

internal sealed class WriteConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(Database.Tables.UserTableName);
        builder.HasKey(user => user.Id);

        ValueConverter<UserName, string> userNameConverter = new(
            u => u.Value,
            u => UserName.Create(u).Value);

        builder.Property(user => user.UserName)
            .HasConversion(userNameConverter);

        ValueConverter<Email, string> emailConverter = new(
            u => u.Value,
            u => Email.Create(u).Value);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasConversion(emailConverter);

        ValueConverter<FirstName, string> firstNameConverter = new(
            u => u.Value,
            u => FirstName.Create(u).Value);

        builder.Property(user => user.FirstName)
            .HasConversion(firstNameConverter!);


        ValueConverter<LastName, string> lastnameConverter = new(
            u => u.Value,
            u => LastName.Create(u).Value);

        builder.Property(user => user.LastName)
            .HasConversion(lastnameConverter!);

        builder.Property(user => user.EmailIsConfirmed);

        builder.Property(typeof(string), "passwordHash");

        builder.Property(u => u.CreatedOnUtc);
        builder.Property(u => u.ModifiedOnUtc);
        builder.Property(user => user.DeletedOnUtc);
        builder.Property(user => user.Deleted);
    }
}
