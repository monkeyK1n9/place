using Microsoft.AspNetCore.Mvc.Infrastructure;
using Place.Api.Application;
using Place.Api.Infrastructure;
using Place.Api.Presentation;
using Place.Api.Presentation.Errors;
using Place.Api.Presentation.Mappings;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();


builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);




WebApplication app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.MapControllers();

    app.UseRouting();
    app.UseInfrastructure();

    if (app.Environment.IsDevelopment())
    {
        app.MapGet("/debug/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
            string.Join("\n", endpointSources.SelectMany(source => source.Endpoints)));
    }
    app.Run();
}
