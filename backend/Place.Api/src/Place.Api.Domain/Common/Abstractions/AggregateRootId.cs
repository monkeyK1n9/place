namespace Place.Api.Domain.Common.Abstractions;

public abstract class AggregateRootId<TId> : ValueObject
{
    public abstract TId Value { get; protected set; }
}
