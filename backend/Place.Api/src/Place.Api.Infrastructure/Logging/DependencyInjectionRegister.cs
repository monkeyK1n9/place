namespace Place.Api.Infrastructure.Logging;

using System;
using System.Linq;
using Destructurama;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Filters;

public static class DependencyInjectionRegister
{
    private const string ConsoleOutputTemplate = "{Timestamp:HH:mm:ss} [{Level:u3}] {Message}{NewLine}{Exception}";

    private const string FileOutputTemplate =
        "{Timestamp:HH:mm:ss} [{Level:u3}] ({SourceContext}.{Method}) {Message}{NewLine}{Exception}";

    private const string SerilogSectionName = "Serilog";

    public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SerilogOptions>(configuration.GetSection(SerilogSectionName));

        return services;
    }

    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder,
        Action<LoggerConfiguration>? configure = null,
        string loggerSectionName = SerilogSectionName)
    {
        builder.Host.AddLogging(configure, loggerSectionName);
        return builder;
    }

    private static IHostBuilder AddLogging(
        this IHostBuilder builder,
        Action<LoggerConfiguration>? configure = null,
        string loggerSectionName = SerilogSectionName
        )
        => builder.UseSerilog((context, loggerConfiguration) =>
        {
            if (string.IsNullOrWhiteSpace(loggerSectionName))
            {
                loggerSectionName = SerilogSectionName;
            }

            SerilogOptions loggerOptions = context.Configuration.BindOptions<SerilogOptions>(loggerSectionName);

            Configure(loggerOptions, loggerConfiguration, context.HostingEnvironment.EnvironmentName);
            configure?.Invoke(loggerConfiguration);
        });

    private static void Configure(
        SerilogOptions serilogOptions,
        LoggerConfiguration loggerConfiguration,
        string environmentName
    )
    {
        LogEventLevel level = GetLogEventLevel(serilogOptions.Level);

        loggerConfiguration.Destructure.UsingAttributes();
        loggerConfiguration
            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                .WithDefaultDestructurers()
                .WithDestructurers(new[] {new DbUpdateExceptionDestructurer()}))
            .Enrich.WithThreadName()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.WithProcessName();
        loggerConfiguration.Enrich.FromLogContext()
            .MinimumLevel.Is(level)
            .Enrich.WithProperty("Environment", environmentName);

        foreach ((string key, object value) in serilogOptions.Tags)
        {
            loggerConfiguration.Enrich.WithProperty(key, value);
        }

        foreach ((string key, string value) in serilogOptions.Overrides)
        {
            LogEventLevel logLevel = GetLogEventLevel(value);
            loggerConfiguration.MinimumLevel.Override(key, logLevel);
        }

        serilogOptions.ExcludePaths?.ToList().ForEach(p => loggerConfiguration.Filter
            .ByExcluding(Matching.WithProperty<string>("RequestPath", n => n.EndsWith(p, StringComparison.Ordinal))));

        serilogOptions.ExcludeProperties?.ToList().ForEach(p => loggerConfiguration.Filter
            .ByExcluding(Matching.WithProperty(p)));

        Configure(loggerConfiguration, serilogOptions);
    }

    private static void Configure(LoggerConfiguration loggerConfiguration, SerilogOptions options)
    {
        SerilogOptions.ConsoleOptions consoleOptions = options.Console;
        SerilogOptions.FileOptions fileOptions = options.File;
        SerilogOptions.SeqOptions seqOptions = options.Seq;

        if (consoleOptions.Enabled)
        {
            loggerConfiguration.WriteTo.Console(outputTemplate: ConsoleOutputTemplate);
        }

        if (fileOptions.Enabled)
        {
            string path = string.IsNullOrWhiteSpace(fileOptions.Path) ? "logs/logs.txt" : fileOptions.Path;
            if (!Enum.TryParse<RollingInterval>(fileOptions.Interval, true, out RollingInterval interval))
            {
                interval = RollingInterval.Day;
            }

            loggerConfiguration.WriteTo.File(path, rollingInterval: interval, outputTemplate: FileOutputTemplate);
        }

        if (seqOptions.Enabled)
        {
            loggerConfiguration.WriteTo.Seq(seqOptions.Url, apiKey: seqOptions.ApiKey);
        }
    }

    private static LogEventLevel GetLogEventLevel(string level)
        => Enum.TryParse<LogEventLevel>(level, true, out LogEventLevel logLevel)
            ? logLevel
            : LogEventLevel.Information;
}
