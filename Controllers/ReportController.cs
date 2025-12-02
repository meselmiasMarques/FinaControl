using FinaControl.Repositories;
using FinaControl.ViewModels.Reports;
using FinaControl.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

namespace FinaControl.Controllers;

[ApiController]
public class ReportController(
    UserRepository userRepository,
    TransactionRepository transactionRepository,
    CategoryRepository categoryRepository)
    : ControllerBase
{
    private readonly UserRepository _userRepository = userRepository;
    private readonly TransactionRepository _transactionRepository = transactionRepository;
    private readonly CategoryRepository _categoryRepository = categoryRepository;
    
    
    [HttpGet("v1/reports")]
    public async Task<IActionResult> GetTransactionsAsync()
    {
        try
        {
            var transactions = await _transactionRepository.GetAsync();
            var users = await (_userRepository).GetAsync();
            var categories = await _categoryRepository.GetAsync();


            var result = (
                from t in transactions
                join u in users on t.UserId equals u.Id
                join c in categories on t.CategoryId equals c.Id
                select (new TransactionsWithUser
                {
                    Id = t.Id,
                    Description = t.Description,
                    Amount = t.Amount,
                    CreatedAt = t.CreatedAt,
                    Type = t.Type == 0 ? "SAÍDA" : "DEPOSITO",
                    Category = c.Name,
                    User = u.Name
                })
            )
                .OrderByDescending(t => t.CreatedAt)
                .ToList();
            
            return Ok(new Response<List<TransactionsWithUser>>(result));
        }
        catch 
        {
            return StatusCode(500, new Response<dynamic>("internal server error"));
        }
    }
}