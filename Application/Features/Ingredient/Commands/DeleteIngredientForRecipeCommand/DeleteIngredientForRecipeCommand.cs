using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Commands.DeleteIngredientForRecipeCommand
{
    public class DeleteIngredientForRecipeCommand : IRequest<DeleteIngredientForRecipeResponse>
    { 
        public Guid IngredientId { get; set; }
        public Guid RecipeId { get; set; }
    }
}
