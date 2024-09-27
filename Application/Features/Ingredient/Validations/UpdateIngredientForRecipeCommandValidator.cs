using Application.Dtos;
using Application.Features.Ingredient.Commands.UpdateIngredientForRecipeCommand;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Validations
{
    public class UpdateIngredientForRecipeCommandValidator : AbstractValidator<RecipeIngredientBaseDto>
    {
        private readonly IBaseRepository<Domain.Entities.Ingredient> _ingredientRepository;
        private readonly IBaseRepository<Domain.Entities.MeasurementUnit> _unitRepository;
        private readonly IBaseRepository<Domain.Entities.Recipe> _recipeRepository;
        public UpdateIngredientForRecipeCommandValidator(
            IBaseRepository<Domain.Entities.Ingredient> ingredientRepository, 
            IBaseRepository<Domain.Entities.MeasurementUnit> unitRepository, 
            IBaseRepository<Domain.Entities.Recipe> recipeRepository
            )

        {
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
            _unitRepository = unitRepository;


            RuleFor(s => s.IngredientId)
                .MustAsync(CheckExistenceIngredient);

            RuleFor(s => s.UnitId)
               .MustAsync(CheckExistenceUnit);

            RuleFor(s => s.RecipeId)
               .MustAsync(CheckExistenceRecipe);
        }


        private async Task<bool> CheckExistenceIngredient(Guid id, CancellationToken token)
        {
            var entity = await _ingredientRepository.GetAsync(id);
            return entity != null;
        }

        private async Task<bool> CheckExistenceUnit(Guid id, CancellationToken token)
        {
            var entity = await _unitRepository.GetAsync(id);
            return entity != null;
        }

        private async Task<bool> CheckExistenceRecipe(Guid id, CancellationToken token)
        {
            var entity = await _recipeRepository.GetAsync(id);
            return entity != null;
        }
    }
}
