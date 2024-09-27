using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IRecipeIngredientRepository : IBaseRepository<RecipeIngredient>
    {
        Task<RecipeIngredient> CheckExistence(Guid recipeId, Guid ingredientId);
        Task<bool> CheckIngredient(Guid guid);
        Task<RecipeIngredient> GetByIds(Guid recipeId, Guid ingredientId);
    }
}
