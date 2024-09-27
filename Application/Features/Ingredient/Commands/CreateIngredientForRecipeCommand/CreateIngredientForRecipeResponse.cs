using Application.Response;

namespace Application.Features.Ingredient.Commands.CreateIngredientForRecipeCommand
{
    public class CreateIngredientForRecipeResponse : BaseResponse
    {
        public Guid RecipeId { get; set; }
    }
}