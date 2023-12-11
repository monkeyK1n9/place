namespace Place.Api.Application.Common.Interfaces.Services;

using System;

/// <summary>
/// Represents a service for providing the current date and time.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Gets the current date and time in Coordinated Universal Time (UTC).
    /// </summary>
    DateTime UtcNow { get; }
}
