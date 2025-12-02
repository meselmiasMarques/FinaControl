using FinaControl.Repositories;
using FinaControl.Services;
using FinaControl.ViewModels.Login;
using Microsoft.AspNetCore.Mvc;

namespace FinaControl.Controllers;

public class AccountController(UserRepository userRepository) : ControllerBase
{
    private readonly UserRepository userRepository = userRepository;

    [HttpGet("v1/accounts")]
    public async Task<IActionResult> PostAsync()
    {
        return null;
    }
    
    [HttpPost("v1/accounts/login")]
    public async Task<IActionResult> LoginAsync(
        [FromServices] TokenService tokenService,
        [FromBody] LoginViewModel model
        )
    {

        var user = await userRepository.GetUserByEmail(model.Email);
        if (user == null)
            return Unauthorized();
        
        var token = tokenService.GenerateToken(user);
        return Ok(token);
    }
    
}