using FinaControl.Extensions;
using FinaControl.Models;
using FinaControl.Repositories;
using FinaControl.Repositories.Abstractions;
using FinaControl.Services;
using FinaControl.ViewModels.Category;
using FinaControl.ViewModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinaControl.Controllers;

[Authorize]
[ApiController]
public class CategoryController(
    ICategoryService categoryService,
  
    UserRepository userRepository
    
) : ControllerBase
{

    [HttpGet("v1/categories")]
    public async Task<ActionResult<Response<List<Category>>>> GetAsync()
    {
        try
        {
            var user = await userRepository.GetUserByEmail(User.Identity.Name);
            var result = await categoryService.GetAsync();

          return Ok(result);
        }
        catch
        {
            return StatusCode(500, new Response<dynamic>("Erro Interno no Servidor"));
        }
    }

    [HttpGet("v1/categories/{id:long}")]
    public async Task<ActionResult<Category>> GetAsync(long id)
    {
        try
        {
            var user = User.Identity.Name;
            var result = await categoryService.GetAsync();
            return result.Errors is not null ?
                BadRequest(new Response<Category>(result.Errors)) 
                : Ok(result);
        }
        catch
        {
            return StatusCode(500, new Response<dynamic>("Erro Interno no Servidor"));
        }
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<dynamic>(ModelState.GetErrors()));
        
        var u =  await userRepository.GetUserByEmail(User.Identity.Name);
        try
        {
           var result = await categoryService.CreateAsync(model);
            return Created($"v1/categories/{result.Data?.Id}",new Response<Category>(result?.Data));
        }
        catch
        {
            return StatusCode(500, new Response<dynamic>("Erro Interno no Servidor"));
        }
    }

    [HttpPut("v1/categories")]
    public async Task<IActionResult> PutAsync(
        [FromBody] EditorCategoryViewModel model,
        [FromRoute] long id
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<dynamic>(ModelState.GetErrors()));
    
        await categoryService.UpdateAsync(model, id);
        return Ok(new Response<dynamic>(model));
    }
    
    [HttpDelete("v1/categories/{id:long}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] long id
    )
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<dynamic>(ModelState.GetErrors()));
        
        try
        {
            var category = await categoryService.GetAsync(id);
            await categoryService.DeleteAsync(id);
           return NoContent();
        }
        catch
        {
            return StatusCode(500, new Response<dynamic>("Erro Interno no Servidor"));
        }
    }
}