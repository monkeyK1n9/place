namespace Place.Api.Infrastructure.Persistence.EF.Authentication.Models.Converters;

using System;
using Domain.Authentication.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

public class UlidToUserIdConverter : ValueConverter<UserId, string>
{
    private static readonly ConverterMappingHints DefaultHints = new(size: 26);

    public UlidToUserIdConverter() : this(null!)
    {
    }

    public UlidToUserIdConverter(ConverterMappingHints mappingHints = null!)
        : base(
            convertToProviderExpression: x => x.Value.ToString(),
            convertFromProviderExpression: x => UserId.Create(Ulid.Parse(x)),
            mappingHints: DefaultHints.With(mappingHints))
    {
    }
}
