using FinaControl.Extensions;
using FinaControl.Models;
using FinaControl.Repositories;
using FinaControl.ViewModels.Category;
using FinaControl.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace FinaControl.Controllers;

[ApiController]
public class CategoryController(CategoryRepository repository) : ControllerBase
{
    private readonly CategoryRepository _repository = repository;


    [HttpGet("v1/categories")]
    public async Task<ActionResult<List<Category>>> GetAsync()
    {
        try
        {
            var categories = await _repository.GetAsync();

            return Ok(new Response<List<Category>>(categories));
        }
        catch 
        {
            return StatusCode(500,new Response<dynamic>("Erro Interno no Servidor"));
        }
    }
    
    [HttpGet("v1/categories/{id:long}")]
    public async Task<ActionResult<Category>> GetAsync(long id)
    {
        try
        {
            var categories = await _repository.GetAsync(id);
            
            if (categories == null)
                return NotFound(new Response<dynamic>("Category not found"));

            return Ok(new Response<Category>(categories));
        }
        catch 
        {
            return StatusCode(500,new Response<dynamic>("Erro Interno no Servidor"));
        }
    }

    [HttpPost("v1/categories")]
    public async Task<IActionResult> PostAsync([FromBody] EditorCategoryViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(new Response<dynamic>(ModelState.GetErrors()));

        var category = new Category
        {
            Id = 0,
            Name = model.Name
        };

        try
        {
            await _repository.CreateAsync(category);
            return Ok(new Response<Category>(category));
        }
        catch 
        {
            return StatusCode(500,new Response<dynamic>("Erro Interno no Servidor"));
        }

    }
}