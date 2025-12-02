using FinaControl.Extensions;
using FinaControl.Models;
using FinaControl.Repositories;
using FinaControl.ViewModels.Response;
using FinaControl.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace FinaControl.Controllers;

[ApiController]
public class UserController(UserRepository repository) : ControllerBase
{
    private readonly UserRepository _repository = repository;
    
    [HttpGet("v1/users")]
    public async Task<IActionResult> GetAsync(
        [FromRoute] int skip = 0, 
        [FromRoute] int take = 25
        )
    {
        try
        {
            var users = await _repository.GetUsersWithRolesEndTransactions(skip, take);
            if (users == null)
                return NotFound(new Response<string>("Não foi encontrado os usuários"));
            
            return Ok(new Response<List<User>>(users));
        }
        catch 
        {
            return StatusCode(500, new Response<string>("Erro Interno no Servidor"));
        }
    }
    
    [HttpGet("v1/users/{id:long}")]
    public async Task<IActionResult> GetAsync(
        [FromRoute] long id
    )
    {
        try
        {
            var user = await _repository.GetAsync(id);
            if (user == null)
                return NotFound(new Response<string>("Usuário Não encontrado"));
            return Ok(new Response<User>(user));
        }
        catch 
        {
            return StatusCode(500, new Response<string>("Erro Interno no Servidor"));
        }
    }
    
    [HttpPost("v1/users")]
    public async Task<IActionResult> PostAsync(
        [FromBody] EditorUserViewModel model
    )
    {
        if (!ModelState.IsValid)
            return StatusCode(404,new Response<string>(null,ModelState.GetErrors()));
        
        var user = new User
        {
            Id = 0,
            Name = model.name,
            Email = model.email,
            PasswordHash = "",
            CreatedAt = DateTime.UtcNow
        };

        try
        {
            await _repository.CreateAsync(user);
            return Created($"v1/users/{user.Id}", new  Response<User>(user));
        }
        catch 
        {
            return StatusCode(500, new Response<string>("Erro Interno no Servidor"));
        }
    }
}