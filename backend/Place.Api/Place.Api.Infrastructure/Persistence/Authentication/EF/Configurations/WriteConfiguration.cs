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

        ValueConverter<UserName, string> UserNameConverter = new ValueConverter<UserName, string>(
            u => u.Value,
            u => UserName.Create(u).Value);

        builder.Property(user => user.UserName)
            .HasConversion(UserNameConverter);

        ValueConverter<Email, string> EmailConverter = new ValueConverter<Email, string>(
            u => u.Value,
            u => Email.Create(u).Value);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasConversion(UserNameConverter);

        ValueConverter<FirstName, string> FirstNameConverter = new ValueConverter<FirstName, string>(
            u => u.Value,
            u => FirstName.Create(u).Value);

        builder.Property(user => user.FirstName)
            .HasConversion(FirstNameConverter!);


        ValueConverter<LastName, string>LastNameConverter = new ValueConverter<LastName, string>(
            u => u.Value,
            u => LastName.Create(u).Value);

        builder.Property(user => user.LastName)
            .IsRequired(false)
            .HasMaxLength(LastName.MaxLength);

        builder.Property(user => user.EmailIsConfirmed);

        builder.Property(typeof(string), "passwordHash");

        builder.Property(u => u.)

        builder.Property(user => user.DeletedOnUtc);
        builder.Property(user => user.Deleted);

    }
}
