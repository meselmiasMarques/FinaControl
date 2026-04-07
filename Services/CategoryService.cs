using FinaControl.Models;
using FinaControl.Repositories.Abstractions;
using FinaControl.ViewModels.Category;
using FinaControl.ViewModels.Response;

namespace FinaControl.Services;

public class CategoryService(
    ICategoryRepository repository,
    
    IUnitOfWork unitOfWork
    
    ) : ICategoryService
{
    public async Task<Response<Category>> CreateAsync(EditorCategoryViewModel model)
    {
        //var user = await userRepository.GetUserByEmail(User.Identity.Name);

        var category = new Category
        {
            Id = 0,
            Name = model.Name,
            UserId = 1
        };
        
        try
        {
            await repository.CreateAsync(category);
            await unitOfWork.CommitAsync();
            return new Response<Category>(category);
        }
        catch (Exception e)
        {
            return new Response<Category>("Ocorreu erro ao Criar categoria.");
        }
    }

    public async Task<Response<Category>> UpdateAsync(EditorCategoryViewModel? entity, long id)
    {
        var category = await repository.GetAsync(id);
        if (category == null)
            return new Response<Category>("Categoria não encontrada");
        
        category.Name = entity.Name;
        
        try
        {
             repository.Update(category);
             await unitOfWork.CommitAsync();
            return new  Response<Category>(category);
        }
        catch
        {
            return  new Response<Category>("Erro Interno no Servidor");
        }
    }

    public async Task<Response<Category>> DeleteAsync(long id)
    {
        var category = await repository.GetAsync(id);
        if (category == null)
            return new Response<Category>("Categoria não encontrada");
        try
        {
            repository.Delete(category);
            await unitOfWork.CommitAsync();
            return new Response<Category>(category);
        }
        catch
        {
            return new Response<Category>("Erro Interno no Servidor");
        }

    }

    public async Task<Response<List<Category>>> GetAsync()
    {
        try
        {
            var categories = await repository.GetAsync();
            return new Response<List<Category>>(categories);
        }
        catch (Exception e)
        {
            return new Response<List<Category>>("Erro ao listar Categorias");
        }
    }
    
    public async Task<Response<Category>> GetAsync(long id)
    {
        try
        {
            var category = await repository.GetAsync(id);
            return new Response<Category>(category);
        }
        catch (Exception e)
        {
            return new Response<Category>("Erro ao listar Categorias");
        }
    }

    public async Task<Response<List<Category>>> GetCategoriesByUserAsync(User user)
    {
        try
        {
            var categories = await repository.GetCategoriesByUserAsync(user);
            return new Response<List<Category>>(categories);
        }
        catch (Exception e)
        {
            return new Response<List<Category>>("Erro ao listar Categorias");
        }
    }
}