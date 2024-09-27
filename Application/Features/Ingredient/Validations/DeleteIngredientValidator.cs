using Application.Features.Ingredient.Commands.DeleteIngredientCommand;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Validations
{
    public class DeleteIngredientValidator : AbstractValidator<DeleteIngredientCommand>
    {
        private readonly IBaseRepository<Domain.Entities.Ingredient> _ingredientRepository;
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;

        public DeleteIngredientValidator(IBaseRepository<Domain.Entities.Ingredient> ingredientRepository, IRecipeIngredientRepository recipeIngredientRepository)
        {
            _ingredientRepository = ingredientRepository;
            _recipeIngredientRepository = recipeIngredientRepository;

            RuleFor(s => s.Id)
                .MustAsync(Exists)
                .WithMessage("Ingredient does Not Exists!");

            RuleFor(s => s.Id)
                .MustAsync(NoActiveRecipesCheck)
                .WithMessage("Ingredient is still present in some recipes! Remove It the active recipes so that you can delete it");


        }

        private async Task<bool> NoActiveRecipesCheck(Guid guid, CancellationToken token)
        {
            var result=  await _recipeIngredientRepository.CheckIngredient(guid);
            return !result;
        }

        private async Task<bool> Exists(Guid guid, CancellationToken token)
        {
            var entity  = await _ingredientRepository.GetAsync(guid);
            return entity != null;
        }
    }
}
