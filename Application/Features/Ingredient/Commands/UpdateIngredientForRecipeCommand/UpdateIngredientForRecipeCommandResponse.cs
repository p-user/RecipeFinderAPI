using Application.Features.Recipe;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Commands.UpdateIngredientForRecipeCommand
{
    public class UpdateIngredientForRecipeCommandResponse : BaseResponse
    {
        public Guid RecipeId { get; set; }
    }
}
