namespace Place.Api.Infrastructure
{
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Extension methods for working with IConfiguration.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Gets strongly typed options from a specific section of the configuration.
        /// </summary>
        /// <typeparam name="TOptions">The type of options to retrieve.</typeparam>
        /// <param name="configuration">The configuration instance.</param>
        /// <param name="sectionName">The name of the configuration section containing the options.</param>
        /// <returns>An instance of the specified options type.</returns>
        public static TOptions GetOptions<TOptions>(this IConfiguration configuration, string sectionName)
            where TOptions : new()
        {
            TOptions? options = new();
            configuration.GetSection(sectionName).Bind(options);
            return options;
        }
    }
}
