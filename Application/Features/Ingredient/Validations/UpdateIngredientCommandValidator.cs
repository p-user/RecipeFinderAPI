using Application.Features.Ingredient.Commands.UpdateIngredientCommand;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Validations
{
    public class UpdateIngredientCommandValidator : AbstractValidator<UpdateIngredientCommand>
    {
        private readonly IBaseRepository<Domain.Entities.Ingredient> _repository;

        public UpdateIngredientCommandValidator(IBaseRepository<Domain.Entities.Ingredient> repository)
        {
            _repository = repository;

            RuleFor(s=>s.Id).NotEmpty().MustAsync(CheckExistence);
            RuleFor(s => s.Ingredient.Name).NotNull();
        }

        private async Task<bool> CheckExistence(Guid id, CancellationToken token)
        { 
            var entity =await _repository.GetAsync(id); 
            return entity != null;
        }
    }
}
