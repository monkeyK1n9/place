namespace Place.Api.Presentation.Controllers;

using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class HomeController : ControllerBase
{
    [HttpGet("/")]
    public string Home() => "It works!";

    [Authorize]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [HttpGet("/protected")]
    public string Protected() => "It works!";
}
