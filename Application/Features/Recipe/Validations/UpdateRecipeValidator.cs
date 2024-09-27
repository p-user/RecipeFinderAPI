using Application.Features.Recipe.Commands.UpdateRecipeCommand;
using Application.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Recipe.Validations
{
    public class UpdateRecipeValidator : AbstractValidator<UpdateRecipeCommand>
    {

        private readonly IBaseRepository<Domain.Entities.Recipe> _repository;
        public UpdateRecipeValidator(IBaseRepository<Domain.Entities.Recipe> baseRepository) 
        {
            _repository = baseRepository;

            RuleFor(p=>p.Recipe.Title).NotEmpty()
                .NotNull()
                .WithMessage("{PropertyName} should have value");

            RuleFor(p => p.Id).NotEmpty()
                .MustAsync(IsLegit);

            
            
        }

        private async Task<bool> IsLegit(Guid guid, CancellationToken token)
        {
            var entity = await _repository.GetAsync(guid);
            return entity != null;
        }

       
    }
}
