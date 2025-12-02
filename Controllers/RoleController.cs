using FinaControl.Extensions;
using FinaControl.Models;
using FinaControl.Repositories;
using FinaControl.ViewModels.Category;
using FinaControl.ViewModels.Response;
using FinaControl.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;

namespace FinaControl.Controllers;

[ApiController]
public class RoleController(RoleRepository repository, UserRepository userRepository) : ControllerBase
{
    private readonly RoleRepository _repository = repository;
    private readonly UserRepository _userRepository =  userRepository;

    [HttpGet("v1/roles")]
    public async Task<ActionResult<List<Role>>> GetAsync()
    {
        try
        {
            var roles = await _repository.GetAsync();

            return Ok(new Response<List<Role>>(roles));
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
            var roles = await _repository.GetAsync(id);
            
            if (roles == null)
                return NotFound(new Response<dynamic>("Roles not found"));

            return Ok(new Response<Role>(roles));
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

        var role = new Role
        {
            Id = 0,
            Name = model.Name
        };

        try
        {
            await _repository.CreateAsync(role);
            return Ok(new Response<Role>(role));
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
            var role = await _repository.GetAsync(id);
            if (role == null)
                return NotFound(new Response<dynamic>("Role not found"));
            
            role.Name = model.Name;
            
            await _repository.UpdateAsync(role);
            return Ok(new Response<Role>(role));
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
            var role = await _repository.GetAsync(id);
            if (role == null)
                return NotFound(new Response<dynamic>("Role not found"));
            
            await _repository.DeleteAsync(role);
            return Ok(new Response<Role>(role));
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
        var  role = await _repository.GetAsync(model.RoleId);
        if (role == null)
            return NotFound(new Response<dynamic>("Role not found"));
        
        var user = await _userRepository.GetAsync(model.UserId);
        if (user == null)
            return NotFound(new Response<dynamic>("User not found"));
        
        if (user.Roles.Any(r => r.Id == model.RoleId))
            return NotFound(new Response<dynamic>("Perfil já está associado ao usuário"));
        
        try
        {
            user.Roles.Add(role);
            await _userRepository.UpdateAsync(user);
            return Ok(new Response<User>(user));
        }
        catch 
        {
            return StatusCode(500,new Response<dynamic>("Erro Interno no Servidor"));
        }
    }
}