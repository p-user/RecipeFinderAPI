using Application.Dtos;
using Application.Features.Ingredient.Commands.CreateIngredientCommand;
using Application.Features.Ingredient.Commands.DeleteIngredientCommand;
using Application.Features.Ingredient.Commands.UpdateIngredientCommand;
using Application.Features.Ingredient.Queries.GetIngredientQuery;
using Application.Features.Ingredient.Queries.GetIngredientsQuery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController :  BasicController
    {

        [HttpGet("{id}")]
        public async Task<ActionResult<GetIngredientQueryResponse>> GetIngredient([FromRoute] Guid id)
        {
            var result = await Mediator.Send(new GetIngredientQuery() { Id = id });
            return Ok(result);
        }


        [HttpGet("All")]
        public async Task<ActionResult<GetIngredientQueryResponse>> GetIngredients()
        {
            var result = await Mediator.Send(new GetIngredientsQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> CreateIngredient([FromBody] IngredientDto dto)
        {
            var result = await Mediator.Send(new CreateIngredientCommand()
            {
                Ingredient = dto

            });
            return CreatedAtAction(nameof(GetIngredient), new { id = result }, result);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<UpdateIngredientResponse>> UpdateIngredient([FromRoute] Guid id, [FromBody] IngredientDto dto)
        {
            var result = await Mediator.Send(new UpdateIngredientCommand()
            {
                Ingredient = dto,
                Id = id

            });
            return CreatedAtAction(nameof(GetIngredient), new { id = result }, result);
        }

        [HttpDelete("{IngredientId}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeleteIngredient([FromRoute] Guid IngredientId)
        {

            var result = await Mediator.Send(new DeleteIngredientCommand()
            {
                Id = IngredientId

            });
            return Ok(result);
        }
    }
}
