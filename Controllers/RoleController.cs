using FinaControl.Extensions;
using FinaControl.Models;
using FinaControl.Repositories.Abstractions;
using FinaControl.Services;
using FinaControl.ViewModels.Response;
using FinaControl.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;

namespace FinaControl.Controllers;

[ApiController]
public class RoleController(
    IRoleService roleService, 
    IUserService userService,
    IUnitOfWork unitOfWork
    ) : ControllerBase
{
    [HttpGet("v1/roles")]
    public async Task<ActionResult<List<Role>>> GetAsync()
    {
        try
        {
            var roles = await roleService.GetAsync();

            return StatusCode(200, roles);
        }
        catch 
        {
            return StatusCode(500,new Response<dynamic>("Erro Interno no Servidor"));
        }
    }
    
    [HttpGet("v1/roles/{id:long}")]
    public async Task<ActionResult<Role>> GetAsync(long id)
    {
        try
        {
            var roles = await roleService.GetAsync(id);
            
            if (roles == null)
                return StatusCode(404,new Response<dynamic>("Roles not found"));

            return StatusCode(200,roles);
        }
        catch 
        {
            return StatusCode(500,new Response<dynamic>("Erro Interno no Servidor"));
        }
    }

    [HttpPost("v1/roles")]
    public async Task<IActionResult> PostAsync([FromBody] EditorRoleViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<dynamic>(ModelState.GetErrors()));

        try
        {
            await roleService.CreateAsync(model);
            return StatusCode(200);
        }
        catch 
        {
            return StatusCode(500,new Response<dynamic>("Erro Interno no Servidor"));
        }

    }
    
    [HttpPut("v1/roles/{id:long}")]
    public async Task<IActionResult> PutAsync([FromBody] EditorRoleViewModel model, 
        [FromRoute] long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<dynamic>(ModelState.GetErrors()));
        
        try
        {
             var result=   await roleService.UpdateAsync(model, id);
             return StatusCode(200,result);
        }
        catch 
        {
            return StatusCode(500,new Response<dynamic>("Erro Interno no Servidor"));
        }

    }
    
    [HttpDelete("v1/roles/{id:long}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] long id)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<dynamic>(ModelState.GetErrors()));
        
        try
        {
           var result = await roleService.DeleteAsync(id);
            return StatusCode(200,result);
        }
        catch 
        {
            return StatusCode(500,new Response<dynamic>("Erro Interno no Servidor"));
        }

    }
    
    [HttpPost("v1/roles/users")]
    public async Task<IActionResult> PostAsync(
        [FromBody] AssociateRoleUserViewModel model
        )
    {
        
        var result = await userService.UpdateRolesByUserAsync(model);
        return StatusCode(200,result);
    }
}