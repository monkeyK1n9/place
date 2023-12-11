namespace Place.Api.Infrastructure.Services;

using System;
using Application.Common.Interfaces.Services;

/// <inheritdoc/>
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
