using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RecipeIngredientRepository :  BaseRepository<RecipeIngredient>, IRecipeIngredientRepository
    {
        public RecipeIngredientRepository(ApiDbContext apiDbContext) : base(apiDbContext)
        {
        }

        public async Task<RecipeIngredient> CheckExistence(Guid recipeId, Guid ingredientId)
        {
            return await _entities.Where(s=>s.IngredientId ==  ingredientId && s.RecipeId == recipeId).FirstOrDefaultAsync();
        }

        public async Task<bool> CheckIngredient(Guid guid)
        {
            return await _entities.AnyAsync(s=>s.IngredientId ==  guid);
        }

        public async Task<RecipeIngredient> GetByIds(Guid recipeId, Guid ingredientId)
        {
            var result = await _entities.Where(s=>s.IngredientId == ingredientId && s.RecipeId == recipeId).FirstOrDefaultAsync();
            return result;
            
        }
    }
}
