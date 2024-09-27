using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Commands.UpdateIngredientForRecipeCommand
{
    public class UpdateIngredientForRecipeCommand :  IRequest<UpdateIngredientForRecipeCommandResponse>
    {
       public RecipeIngredientBaseDto RecipeIngredient { get; set; }
       public Guid Id { get; set; }
    }
}
