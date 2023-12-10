namespace Place.Api.Presentation.Controllers;

using Microsoft.AspNetCore.Mvc;

public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public string Home() => "It works!";
}
