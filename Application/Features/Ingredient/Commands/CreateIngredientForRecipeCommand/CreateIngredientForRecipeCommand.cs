
using Application.Dtos;
using MediatR;


namespace Application.Features.Ingredient.Commands.CreateIngredientForRecipeCommand
{
    public class CreateIngredientForRecipeCommand : IRequest<CreateIngredientForRecipeResponse>
    {

        public RecipeIngredientBaseDto RecipeIngredient { get; set; }
    }
}
