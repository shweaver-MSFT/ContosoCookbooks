using Contoso.Core.Services.DataProviders;
using Contoso.Core.Models.Data;
using Contoso.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contoso.Data.Mock
{
    public class MockCookbookDataProvider : ICookbookDataProvider
    {
        private static readonly List<ICookbookModel> _cookbooks = [
            new CookbookModel("1", "Family Cookbook"),
            new CookbookModel("2", "Other Cookbook")
            ];

        private static readonly List<IRecipeModel> _recipes = [
            new RecipeModel("1", "1", "PB&J", "Classic and simple.", TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(0)),
            new RecipeModel("2", "1", "Cowboy Crack", "Good for a party.", TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(120)),
            new RecipeModel("3", "1", "Cheddar Bay Biscuit Chicken Pot Pie", "Hearty, creamy filling and topped with cheesy golden biscuits.", TimeSpan.FromMinutes(20), TimeSpan.FromMinutes(20)),
            
            new RecipeModel("4", "2", "Cranberry Fluff", "A holiday favorite.", TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(0)),
            ];

        private static readonly List<IIngredientModel> _ingredients = [
            new IngredientModel("1", "1", "Peanut butter", new MeasurementModel(MeasurementType.Tablespoon, PreparationType.Spread, 2)),
            new IngredientModel("2", "1", "Jelly", new MeasurementModel(MeasurementType.Tablespoon, PreparationType.Spread, 2)),
            new IngredientModel("3", "1", "Bread", new MeasurementModel(MeasurementType.Piece, PreparationType.Sliced, 2)),

            new IngredientModel("4", "2", "Cream cheese", new MeasurementModel(MeasurementType.Ounce, PreparationType.Cubed, 16)),
            new IngredientModel("5", "2", "Mozerella", new MeasurementModel(MeasurementType.Cup, PreparationType.Shredded, 2)),
            new IngredientModel("6", "2", "Ground sausage", new MeasurementModel(MeasurementType.Pound, PreparationType.Browned, 1)),
            new IngredientModel("7", "2", "White corn", new MeasurementModel(MeasurementType.Ounce, PreparationType.Drained, 11)),
            new IngredientModel("8", "2", "Rotel tomatoes and chiles", new MeasurementModel(MeasurementType.Can, PreparationType.None, 2)),

            new IngredientModel("9", "3", "Cooked chicken", new MeasurementModel(MeasurementType.Cup, PreparationType.Shredded, 4)),
            new IngredientModel("10", "3", "Celery", new MeasurementModel(MeasurementType.Cup, PreparationType.Chopped, 2)),
            new IngredientModel("11", "3", "Carrots", new MeasurementModel(MeasurementType.Cup, PreparationType.Chopped, 2)),
            new IngredientModel("12", "3", "Yellow onion", new MeasurementModel(MeasurementType.Whole, PreparationType.Chopped, 1)),
            new IngredientModel("13", "3", "Butter", new MeasurementModel(MeasurementType.Tablespoon, PreparationType.None, 3)),
            new IngredientModel("14", "3", "Flour", new MeasurementModel(MeasurementType.Cup, PreparationType.None, .25)),
            new IngredientModel("15", "3", "Poultry seasoning", new MeasurementModel(MeasurementType.Teaspoon, PreparationType.None, 2)),
            new IngredientModel("16", "3", "Chicken broth", new MeasurementModel(MeasurementType.Cup, PreparationType.None, 2)),
            new IngredientModel("17", "3", "Heavy cream", new MeasurementModel(MeasurementType.Cup, PreparationType.None, 1)),
            new IngredientModel("18", "3", "Cheddar Bay biscuit mix", new MeasurementModel(MeasurementType.Cup, PreparationType.None, 2.25)),
            new IngredientModel("19", "3", "Milk", new MeasurementModel(MeasurementType.Cup, PreparationType.None, .75)),
            new IngredientModel("20", "3", "Sharp cheddar cheese", new MeasurementModel(MeasurementType.Cup, PreparationType.Shredded, .5)),
            new IngredientModel("21", "3", "Butter", new MeasurementModel(MeasurementType.Cup, PreparationType.Melted, .25)),

            new IngredientModel("22", "4", "Fresh cranberries", new MeasurementModel(MeasurementType.Ounce, PreparationType.Chopped, 12)),
            new IngredientModel("23", "4", "Sugar", new MeasurementModel(MeasurementType.Cup, PreparationType.None, .75)),
            new IngredientModel("24", "4", "Crushed pineapple", new MeasurementModel(MeasurementType.Ounce, PreparationType.Drained, 8)),
            new IngredientModel("25", "4", "Grapes", new MeasurementModel(MeasurementType.Cup, PreparationType.Sliced, 1)),
            new IngredientModel("26", "4", "Pecans", new MeasurementModel(MeasurementType.Cup, PreparationType.Chopped, 1)),
            new IngredientModel("27", "4", "Mini marshmallows", new MeasurementModel(MeasurementType.Cup, PreparationType.None, 2)),
            new IngredientModel("28", "4", "Cool Whip", new MeasurementModel(MeasurementType.Cup, PreparationType.None, 3)),
            ];

        public async Task AddCookbookAsync(ICookbookModel cookbook)
        {
            await Task.Delay(2000);
            _cookbooks.Add(cookbook);
        }

        public async Task AddIngredientAsync(IIngredientModel ingredient)
        {
            await Task.Delay(2000);
            _ingredients.Add(ingredient);
        }

        public async Task AddRecipeAsync(IRecipeModel recipe)
        {
            await Task.Delay(2000);
            _recipes.Add(recipe);
        }

        public async Task<ICookbookModel> GetCookbookAsync(string cookbookId)
        {
            await Task.Delay(2000);

            foreach (ICookbookModel cookbook in _cookbooks)
            {
                if (cookbook.Id == cookbookId)
                {
                    return cookbook;
                }
            }

            throw new KeyNotFoundException($"Key not found: {cookbookId}");
        }

        public async Task<IList<ICookbookModel>> GetCookbooksAsync()
        {
            await Task.Delay(2000);
            return _cookbooks;
        }

        public async Task<IIngredientModel> GetIngredientAsync(string ingredientId)
        {
            await Task.Delay(2000);

            foreach (IIngredientModel ingredient in _ingredients)
            {
                if (ingredient.Id == ingredientId)
                {
                    return ingredient;
                }
            }

            throw new KeyNotFoundException($"Key not found: {ingredientId}");
        }

        public async Task<IList<IIngredientModel>> GetIngredientsAsync(string recipeId)
        {
            await Task.Delay(2000);

            IList<IIngredientModel> ingredients = [];
            foreach (IIngredientModel ingredient in _ingredients)
            {
                if (ingredient.ParentId == recipeId)
                {
                    ingredients.Add(ingredient);
                }
            }

            return ingredients;
        }

        public async Task<IRecipeModel> GetRecipeAsync(string recipeId)
        {
            await Task.Delay(2000);

            foreach (IRecipeModel recipe in _recipes)
            {
                if (recipe.Id == recipeId)
                {
                    return recipe;
                }
            }

            throw new KeyNotFoundException($"Key not found: {recipeId}");
        }

        public async Task<IList<IRecipeModel>> GetRecipesAsync(string cookbookId)
        {
            await Task.Delay(2000);

            IList<IRecipeModel> recipes = [];
            foreach (IRecipeModel recipe in _recipes)
            {
                if (recipe.ParentId == cookbookId)
                {
                    recipes.Add(recipe);
                }
            }

            return recipes;
        }

        public async Task RemoveCookbookAsync(string cookbookId)
        {
            await Task.Delay(2000);
            _cookbooks.RemoveAll(c => c.Id == cookbookId);
        }

        public async Task RemoveIngredientAsync(string ingredientId)
        {
            await Task.Delay(2000);
            _ingredients.RemoveAll(i => i.Id == ingredientId);
        }

        public async Task RemoveRecipeAsync(string recipeId)
        {
            await Task.Delay(2000);
            _recipes.RemoveAll(r => r.Id == recipeId);
        }

        public async Task UpdateCookbookAsync(ICookbookModel cookbook)
        {
            await Task.Delay(2000);

            for (int x = 0; x < _cookbooks.Count; x++)
            {
                ICookbookModel c = _cookbooks[x];
                if (c.Id == cookbook.Id)
                {
                    _cookbooks[x] = cookbook;
                    return;
                }
            }
        }

        public async Task UpdateIngredientAsync(IIngredientModel ingredient)
        {
            await Task.Delay(2000);

            for (int x = 0; x < _ingredients.Count; x++)
            {
                IIngredientModel i = _ingredients[x];
                if (i.Id == ingredient.Id)
                {
                    _ingredients[x] = ingredient;
                    return;
                }
            }
        }

        public async Task UpdateRecipeAsync(IRecipeModel recipe)
        {
            await Task.Delay(2000);

            for (int x = 0; x < _recipes.Count; x++)
            {
                IRecipeModel r = _recipes[x];
                if (r.Id == recipe.Id)
                {
                    _recipes[x] = recipe;
                    return;
                }
            }
        }
    }
}
