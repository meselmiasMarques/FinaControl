using FinaControl.Extensions;
using FinaControl.Models;
using FinaControl.Repositories;
using FinaControl.Services;
using FinaControl.ViewModels.Login;
using FinaControl.ViewModels.Register;
using FinaControl.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinaControl.Controllers;

public class AccountController(UserRepository userRepository) : ControllerBase
{
    private readonly UserRepository userRepository = userRepository;

    [HttpPost("v1/accounts")]
    public async Task<IActionResult> PostAsync(
        [FromBody] RegisterViewModel model
        )
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<dynamic>(null,ModelState.GetErrors()));

        var user = new User
        {
            Id = 0,
            Email = model.Email,
            Name =  model.Name,
            PasswordHash = "123456",
            CreatedAt = DateTime.UtcNow
        };
        
        try
        {
            await userRepository.CreateAsync(user);
            return Ok(new Response<User>(user));
        }
        catch (DbUpdateException e)
        {
            return StatusCode(404,new Response<dynamic>("O email já está cadastrado"));
        }
        catch (Exception e)
        {
            return StatusCode(500,new Response<dynamic>("O email já está cadastrado"));

        }
        
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