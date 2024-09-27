using Application.Features.Ingredient.Commands.DeleteIngredientForRecipeCommand;
using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Validations
{
    public class DeleteIngredientForRecipeValidator : AbstractValidator<DeleteIngredientForRecipeCommand>
    {
        private readonly IRecipeIngredientRepository _recipeingredientRepository;


        public DeleteIngredientForRecipeValidator(IRecipeIngredientRepository recipeingredientRepository)
        {
            
            _recipeingredientRepository = recipeingredientRepository;

            RuleFor(s => new { s.IngredientId, s.RecipeId })
               .MustAsync(async (ids, cancellation) => await Exists(ids.RecipeId, ids.IngredientId))
                .WithMessage("The ingredient for this recipe does NOT exist!");

        }

        private async Task<bool> Exists(Guid recipeId, Guid ingredientId)
        {
           var result = await _recipeingredientRepository.CheckExistence(recipeId, ingredientId);
            return result is not null; 
        }

        
    }
}
