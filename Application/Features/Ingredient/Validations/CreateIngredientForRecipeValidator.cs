using Application.Dtos;
using Application.Features.Ingredient.Commands.CreateIngredientForRecipeCommand;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Validations
{
    public class CreateIngredientForRecipeValidator : AbstractValidator<RecipeIngredientBaseDto>
    {
        private readonly IBaseRepository<Domain.Entities.Ingredient> _ingredientRepository;
        private readonly IBaseRepository<Domain.Entities.Recipe> _recipesRepository;
        private readonly IBaseRepository<Domain.Entities.MeasurementUnit> _unitRepository;

        public CreateIngredientForRecipeValidator(IBaseRepository<Domain.Entities.Ingredient> ingredientRepository, IBaseRepository<Domain.Entities.Recipe> recipesRepository, IBaseRepository<Domain.Entities.MeasurementUnit> unitRepository)
        {
            _ingredientRepository = ingredientRepository;
            _recipesRepository = recipesRepository;
            _unitRepository = unitRepository;

            RuleFor(s => s.IngredientId)
                .MustAsync(ExistsIngredient);

            RuleFor(s => s.RecipeId)
               .MustAsync(ExistsRecipe);

            RuleFor(s => s.UnitId)
              .MustAsync(ExistsUnit);
        }


        private async Task<bool> ExistsIngredient(Guid ingredientId, CancellationToken cancellationToken)
        {
            var entity = await _ingredientRepository.GetAsync(ingredientId);
            return entity != null;
        }

        private async Task<bool> ExistsRecipe(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _recipesRepository.GetAsync(id);
            return entity != null;
        }

        private async Task<bool> ExistsUnit(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _unitRepository.GetAsync(id);
            return entity != null;
        }
    }
}
