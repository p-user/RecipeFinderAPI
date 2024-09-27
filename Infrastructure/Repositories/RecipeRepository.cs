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
    public class RecipeRepository : BaseRepository<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApiDbContext apiDbContext) : base(apiDbContext)
        {
        }

        public async Task<Recipe> GetRecipeWithIngredients(Guid id)
        {
            return  await _context.Recipes.Include(s=>s.Ingredients).FirstOrDefaultAsync(s=>s.Id==id);
        }
    }
}
