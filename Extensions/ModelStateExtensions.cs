using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FinaControl.Extensions;

public static class ModelStateExtensions
{
    public static List<string> GetErrors(this ModelStateDictionary modelState)
    {
        return (from item in modelState.Values from error in item.Errors select error.ErrorMessage).ToList();
    }
}