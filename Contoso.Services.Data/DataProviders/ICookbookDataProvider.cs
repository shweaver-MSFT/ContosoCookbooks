using Contoso.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Services.Data.DataProviders
{
    public interface ICookbookDataProvider
    {
        // Cookbooks
        Task<ICookbookModel> GetCookbookAsync(string cookbookId);

        Task<IList<ICookbookModel>> GetCookbooksAsync();

        Task AddCookbookAsync(ICookbookModel cookbook);

        Task UpdateCookbookAsync(ICookbookModel cookbook);

        Task RemoveCookbookAsync(string cookbookId);

        // Recipes
        Task<IRecipeModel> GetRecipeAsync(string recipeId);

        Task<IList<IRecipeModel>> GetRecipesAsync(string cookbookId);

        Task AddRecipeAsync(IRecipeModel recipe);

        Task UpdateRecipeAsync(IRecipeModel recipe);

        Task RemoveRecipeAsync(string recipeId);

        // Ingredients
        Task<IIngredientModel> GetIngredientAsync(string ingredientId);

        Task<IList<IIngredientModel>> GetIngredientsAsync(string recipeId);

        Task AddIngredientAsync(IIngredientModel ingredient);

        Task UpdateIngredientAsync(IIngredientModel ingredient);

        Task RemoveIngredientAsync(string ingredientId);
    }
}
