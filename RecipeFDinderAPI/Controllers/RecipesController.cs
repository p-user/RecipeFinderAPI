using Application.Constants;
using Application.Dtos;
using Application.Features.Recipe.Commands.CreateRecipeCommand;
using Application.Features.Recipe.Queries.GetRecipe;
using Application.Features.Recipe.Queries.GetRecipes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RecipeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : BasicController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<GetRecipesResponse>> GetAllRecipes()
        {
            var result = await Mediator.Send(new GetRecipesQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GetRecipesResponse>> GetRecipe([FromRoute] Guid id)
        {
            var result = await Mediator.Send(new GetRecipeQuery() { Id = id });
            return Ok(result);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<GetRecipeResponse>> CreateRecipe(RecipeDto dto)
        {
            var result = await Mediator.Send(new CreateRecipeCommand()
            {
                Recipe = dto

            });
            return CreatedAtAction(nameof(GetRecipe), new { id = result }, result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<GetRecipeResponse>> UpdateRecipe([FromRoute] Guid id, [FromBody]RecipeDto dto)
        {
            var result = await Mediator.Send(new CreateRecipeCommand()
            {
                Recipe = dto

            });
            return CreatedAtAction(nameof(GetRecipe), new { id = result }, result);
        }
    }
}
