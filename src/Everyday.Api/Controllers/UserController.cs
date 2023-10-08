using Microsoft.AspNetCore.Mvc;
using Everyday.Messages;
using Everyday.Services;

namespace Everyday.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(
        ILogger<UserController> logger,
        IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpPost(Name = "Create")]
    public async Task<IActionResult> CreateUser(UserCreated userCreated)
    {
        await _userService.CreateUserAsync(userCreated);
        return Ok();
    }
}
