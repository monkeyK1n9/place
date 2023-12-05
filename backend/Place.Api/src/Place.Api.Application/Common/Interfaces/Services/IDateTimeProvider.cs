namespace Place.Api.Application.Common.Interfaces.Services;

using System;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
