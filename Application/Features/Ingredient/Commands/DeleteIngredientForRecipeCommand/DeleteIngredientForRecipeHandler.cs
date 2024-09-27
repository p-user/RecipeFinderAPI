using Application.Features.Ingredient.Validations;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Commands.DeleteIngredientForRecipeCommand
{
    public class DeleteIngredientForRecipeHandler : IRequestHandler<DeleteIngredientForRecipeCommand, DeleteIngredientForRecipeResponse>
    {
        private readonly IRecipeIngredientRepository _recipeIngredientRepository;
        private readonly ILogger<DeleteIngredientForRecipeHandler> _logger;

        public DeleteIngredientForRecipeHandler(IRecipeIngredientRepository recipeIngredientRepository, ILogger<DeleteIngredientForRecipeHandler> logger)
        {
            _recipeIngredientRepository = recipeIngredientRepository;
            _logger = logger;
        }

        public async  Task<DeleteIngredientForRecipeResponse> Handle(DeleteIngredientForRecipeCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteIngredientForRecipeResponse();
            var validator = new DeleteIngredientForRecipeValidator(_recipeIngredientRepository);
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

                var entity = await _recipeIngredientRepository.GetByIds(request.RecipeId,request.IngredientId );
                _recipeIngredientRepository.Delete(entity);
                await _recipeIngredientRepository.SaveChangesAsync();

                _logger.LogInformation($"Ingredient with id {request.IngredientId}, deleted successfully from recipe {request.IngredientId}");

                response.Success = true;
                response.Message = $"Ingredient with id {request.IngredientId}, deleted successfully from recipe {request.IngredientId}";
                return response;
            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}
