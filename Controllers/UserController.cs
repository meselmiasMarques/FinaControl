using FinaControl.Extensions;
using FinaControl.Models;
using FinaControl.Repositories;
using FinaControl.Repositories.Abstractions;
using FinaControl.Services;
using FinaControl.ViewModels.Response;
using FinaControl.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace FinaControl.Controllers;

[ApiController]
public class UserController(IUserService userService) : ControllerBase
{
    
    [HttpGet("v1/users")]
    public async Task<IActionResult> GetAsync(
        [FromRoute] int skip = 0, 
        [FromRoute] int take = 25
        )
    {
        try
        {
            var users = await userService.GetUsersWithRolesEndTransactions(skip, take);
            if (users == null)
                return NotFound(new Response<string>("Não foi encontrado os usuários"));
            
            return StatusCode(200, users);
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
            var user = await userService.GetAsync(id);
            if (user == null)
                return StatusCode(404,new Response<string>("Usuário Não encontrado"));
            return StatusCode(200, user);
        }
        catch 
        {
            return StatusCode(500, new Response<string>("Erro Interno no Servidor"));
        }
    }
}