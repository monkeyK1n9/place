namespace Place.Api.Infrastructure.Swagger.Attributes;

using System;

[AttributeUsage(AttributeTargets.Property)]
// ReSharper disable once ClassNeverInstantiated.Global
#pragma warning disable CA1813
public class HiddenAttribute : Attribute;
#pragma warning restore CA1813
