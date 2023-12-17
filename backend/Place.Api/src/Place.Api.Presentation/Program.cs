using Place.Api.Application;
using Place.Api.Infrastructure;
using Place.Api.Presentation;

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
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseInfrastructure();
    app.Run();
}
