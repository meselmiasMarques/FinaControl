using FinaControl.Models;
using FinaControl.ViewModels.Category;
using FinaControl.ViewModels.Response;

namespace FinaControl.Services;

public interface ICategoryService
{
    Task<Response<Category>> CreateAsync(EditorCategoryViewModel model);
    Task<Response<Category>> UpdateAsync(EditorCategoryViewModel entity, long id);
    Task<Response<Category>> DeleteAsync(long id);
    Task<Response<List<Category>>> GetAsync();
    Task<Response<Category>> GetAsync(long id);
    Task<Response<List<Category>>> GetCategoriesByUserAsync(User user);
}