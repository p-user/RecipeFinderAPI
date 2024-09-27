using Application.Features.Ingredient.Commands.DeleteIngredientForRecipeCommand;
using Application.Features.Ingredient.Validations;
using Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Commands.DeleteIngredientCommand
{
    public class DeleteIngredientHandler : IRequestHandler<DeleteIngredientCommand, DeleteIngredientResponse>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ILogger<DeleteIngredientForRecipeHandler> _logger;
        private readonly IBaseRepository<Domain.Entities.Ingredient> _ingredientRepository;

      

        public DeleteIngredientHandler(IRecipeIngredientRepository recipeIngredientRepository, ILogger<DeleteIngredientForRecipeHandler> logger, IBaseRepository<Domain.Entities.Ingredient> ingredientRepository)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _logger = logger;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<DeleteIngredientResponse> Handle(DeleteIngredientCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteIngredientResponse();
            var validator = new DeleteIngredientValidator(_ingredientRepository,_recipeIngredientRepository);
            try
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (validationResult.Errors.Any())
                {
                    response.Success = false;
                    response.ValidationErrors = new();
                    foreach (var error in validationResult.Errors)
                    {
                        response.ValidationErrors.Add(error.ErrorMessage);
                    }
                    return response;
                }

                var entity = await _ingredientRepository.GetAsync(request.Id);
                _ingredientRepository.Delete(entity);
                await _ingredientRepository.SaveChangesAsync();

                _logger.LogInformation($"Ingredient with id {request.Id}, deleted successfully!");

                response.Success = true;
                response.Message = $"Ingredient with id {request.Id}, deleted successfully!";
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
