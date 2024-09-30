using Microsoft.AspNetCore.Mvc;
using Application.Features.Ingredient.Commands.UpdateIngredientCommand;
using Application.Dtos;
using Application.Features.Ingredient.Commands.UpdateIngredientForRecipeCommand;
using Application.Features.Recipe.Queries.RecipeWithIngredientQuery;
using Application.Features.Ingredient.Commands.CreateIngredientForRecipeCommand;
using Application.Features.Ingredient.Commands.DeleteIngredientForRecipeCommand;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;

namespace RecipeFinderAPI.Controllers
{
    [Route("api/Recipes/{RecipeId}")]
    [ApiController]
    public class RecipeIngredientsController : BasicController
    {

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        public async Task<ActionResult> CreateIngredientForRecipe([FromRoute] Guid RecipeId, [FromBody] RecipeIngredientBaseDto dto)
        {
            if (RecipeId != dto.RecipeId)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new CreateIngredientForRecipeCommand()
            {
                RecipeIngredient = dto

            });
            return Ok(result);
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateIngredientForRecipe([FromRoute] Guid id, [FromRoute] Guid RecipeId, [FromBody] RecipeIngredientBaseDto dto)
        {
            if (RecipeId != dto.RecipeId)
            {
                return BadRequest();
            }
            var result = await Mediator.Send(new UpdateIngredientForRecipeCommand()
            {
                RecipeIngredient = dto,
                Id = id


            });
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetRecipeWithIngredients([FromRoute] Guid RecipeId)
        {
            var result = await Mediator.Send(new GetRecipeWithIngredientQuery()
            {
                Id = RecipeId
            });

            return Ok(result);

        }

        [HttpDelete("{IngredientId}")]
        [Authorize]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteIngredientForRecipe([FromRoute] Guid RecipeId, [FromRoute] Guid IngredientId)
        {
            
            var result = await Mediator.Send(new DeleteIngredientForRecipeCommand()
            {
                IngredientId = IngredientId,
                RecipeId = RecipeId

            });
            return Ok(result);
        }
    }
}
