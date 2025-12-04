using System.Runtime.InteropServices.JavaScript;
using FinaControl.Extensions;
using FinaControl.Models;
using FinaControl.Models.Enums;
using FinaControl.Repositories;
using FinaControl.ViewModels.Response;
using FinaControl.ViewModels.Transaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinaControl.Controllers;

[Authorize()]
[ApiController]
public class TransactionController(TransactionRepository repository,UserRepository userRepository) : ControllerBase
{
    private readonly TransactionRepository _repository = repository;
    
    [HttpGet("v1/transactions")]
    public async Task<IActionResult> GetAsync(
        [FromQuery] int skip = 0, 
        [FromQuery] int take = 25)
    {
        try
        {
            var user =  await userRepository.GetUserByEmail(User.Identity.Name);
            
            var transactions = await _repository
                .GetTransactionByUserAsync(skip, take,user);

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
        
        try
        {
            var user =  await userRepository.GetUserByEmail(User.Identity.Name);
            
            var transaction = new Transaction
            {
                Id = 0,
                Description = model.Description,
                Amount =  model.Amount,
                Type = ETransactionType.Widthdrawal,
                UserId = user.Id,
                CategoryId =  model.CategoryId,
                CreatedAt = DateTime.UtcNow,
                Payment = null
            };
            
            await _repository.CreateAsync(transaction);
            return Created($"v1/transactions/{transaction.Id}",new Response<Transaction>(transaction));
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
            transaction.Description = model.Description;
            transaction.Type = ETransactionType.Widthdrawal;
            transaction.UserId = 1;
            transaction.CategoryId = model.CategoryId;
            transaction.Payment = DateTime.UtcNow;

            await _repository.UpdateAsync(transaction);
            return Ok(new Response<Transaction>(transaction));
        }
        catch 
        {
            return StatusCode(500, new Response<string>("Erro Interno no Servidor"));
        }

    }
    
    [HttpPut("v1/transactions/payment/{id:long}")]
    public async Task<IActionResult> PutAsync(
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
            
            transaction.Payment = DateTime.UtcNow;

            await _repository.UpdateAsync(transaction);
            return Ok(new Response<Transaction>(transaction));
        }
        catch 
        {
            return StatusCode(500, new Response<string>("Erro Interno no Servidor"));
        }

    }
    
    [HttpDelete("v1/transactions/{id:long}")]
    public async Task<IActionResult> DeleteAsync(
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
            
            await _repository.DeleteAsync(transaction);
            return Ok(new Response<Transaction>(transaction));
        }
        catch 
        {
            return StatusCode(500, new Response<string>("Erro Interno no Servidor"));
        }

    }
}