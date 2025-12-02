using FinaControl.Extensions;
using FinaControl.Models;
using FinaControl.Repositories;
using FinaControl.ViewModels.Response;
using FinaControl.ViewModels.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace FinaControl.Controllers;

[ApiController]
public class TransactionController(TransactionRepository repository) : ControllerBase
{
    private readonly TransactionRepository _repository = repository;

    [HttpGet("v1/transactions")]
    public async Task<IActionResult> GetAsync(
        [FromQuery] int skip = 0, 
        [FromQuery] int take = 25)
    {
        try
        {
            var transactions = await _repository
                .GetTransactionWithCategoriesAsync(skip, take);

            return Ok(new Response<List<Transaction>>(transactions));
        }
        catch 
        {
            return StatusCode(500, new Response<string>("Erro Interno no Servidor"));
        }
    }
    
    [HttpGet("v1/transactions/{id}")]
    public async Task<IActionResult> GetAsync(
       long id)
    {
        try
        {
            var transaction = await _repository.GetAsync(id);
            
            if (transaction == null)
                return NotFound(new Response<string>("Transaction not found"));

            return Ok(new Response<Transaction>(transaction));
        }
        catch (Exception e)
        {
            return StatusCode(500, new Response<string>("Erro Interno no Servidor"));
        }
    }

    [HttpPost("v1/transactions")]
    public async Task<IActionResult> PostAsync(
        [FromBody] EditorTransactionViewModel model
        )
    {
        if (!ModelState.IsValid)
            return StatusCode(404,new Response<string>(null,ModelState.GetErrors()));

        var transaction = new Transaction
        {
            Id = 0,
            Amount =  model.Amount,
            Type = model.Type,
            UserId = 1 //alterar para pegar o usuario logado
            ,CategoryId =  model.CategoryId,
            CreatedAt = DateTime.UtcNow
        };
        
        try
        {
            await _repository.CreateAsync(transaction);
            return Ok(new Response<Transaction>(transaction));
        }
        catch 
        {
            return StatusCode(500, new Response<string>("Erro Interno no Servidor"));
        }

    }
    
    [HttpPut("v1/transactions/{id:long}")]
    public async Task<IActionResult> PutAsync(
        [FromBody] EditorTransactionViewModel model,
        [FromRoute] long id
    )
    {
        if (!ModelState.IsValid)
            return StatusCode(404,new Response<string>(null,ModelState.GetErrors()));

        try
        {
            var transaction = await _repository.GetAsync(id);
            if (transaction == null)
                return NotFound(new Response<string>("Transaction not found"));

            transaction.Amount = model.Amount;
            transaction.Type = model.Type;
            transaction.UserId = 1;
            transaction.CategoryId = model.CategoryId;

            await _repository.UpdateAsync(transaction);
            return Ok(new Response<Transaction>(transaction));
        }
        catch 
        {
            return StatusCode(500, new Response<string>("Erro Interno no Servidor"));
        }

    }
}