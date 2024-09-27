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

namespace Application.Features.Ingredient.Commands.UpdateIngredientForRecipeCommand
{
    public class UpdateIngredientForRecipeCommandHandler : IRequestHandler<UpdateIngredientForRecipeCommand, UpdateIngredientForRecipeCommandResponse>
    {

        private readonly IMapper mapper;
        private readonly ILogger<UpdateIngredientForRecipeCommandHandler> logger;
        private readonly IRecipeIngredientRepository  recipeIngredientRepository;
        private readonly IBaseRepository<Domain.Entities.Recipe>  recipeRepository;
        private readonly IBaseRepository<Domain.Entities.MeasurementUnit>  unitRepository;
        private readonly IBaseRepository<Domain.Entities.Ingredient>  ingredientRepository;

        public UpdateIngredientForRecipeCommandHandler(IMapper mapper, ILogger<UpdateIngredientForRecipeCommandHandler> logger, IRecipeIngredientRepository recipeIngredientRepository, IBaseRepository<Domain.Entities.MeasurementUnit> unitRepo, IBaseRepository<Domain.Entities.Ingredient> ingredientRepo , IBaseRepository<Domain.Entities.Recipe> recipeRepo)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.recipeIngredientRepository = recipeIngredientRepository;
             recipeRepository = recipeRepo;
            unitRepository = unitRepo;
            ingredientRepository = ingredientRepo;
        }

        public async Task<UpdateIngredientForRecipeCommandResponse> Handle(UpdateIngredientForRecipeCommand request, CancellationToken cancellationToken)
        {
           var response  = new UpdateIngredientForRecipeCommandResponse();
            var validator = new UpdateIngredientForRecipeCommandValidator(ingredientRepository, unitRepository, recipeRepository);
            try
            {
                var validationResult = await validator.ValidateAsync(request.RecipeIngredient, cancellationToken);
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

                var entity = await recipeIngredientRepository.GetAsync(request.Id);
                    if(entity is null)
                    {
                        throw new Exception("Ingredient not defined for this recipe");
                    }
                     mapper.Map(entity, request.RecipeIngredient);
                    recipeIngredientRepository.UpdateAsync(entity);
                    await recipeIngredientRepository.SaveChangesAsync();
                    response.RecipeId = entity.RecipeId;
                

            }
            catch (Exception ex)
            {
                response.Success =false;
                response.ValidationErrors = new();
                response.ValidationErrors.Add(ex.Message);
            }
            return response;
        }
    }
}
