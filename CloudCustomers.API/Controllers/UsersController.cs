using CloudCustomers.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CloudCustomers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{

    private readonly ILogger<UsersController> _logger;
    private readonly IUsersSevice _userService;

    public UsersController(IUsersSevice usersSevice)
    {
        this._userService = usersSevice;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllUsers();

        if (users.Any()) {
            return Ok(users);
        }

        return NotFound();
    }

}