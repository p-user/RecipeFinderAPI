using Application.Features.Ingredient.Validations;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Ingredient.Commands.CreateIngredientForRecipeCommand
{

   
    public class CreateIngredientForRecipeHandler : IRequestHandler<CreateIngredientForRecipeCommand, CreateIngredientForRecipeResponse>
    {
        private readonly IBaseRepository<Domain.Entities.Ingredient> _ingredientRepository;
        private readonly IBaseRepository<MeasurementUnit> _unitRepository;
        private readonly IBaseRepository<Domain.Entities.Recipe> _recipesRepository;
        private readonly IBaseRepository<RecipeIngredient> _recipeIngredientRepository;
        private readonly ILogger<CreateIngredientForRecipeHandler> _logger;
        private readonly IMapper _mapper;

        public CreateIngredientForRecipeHandler(IBaseRepository<Domain.Entities.Ingredient> ingredientRepository, IBaseRepository<MeasurementUnit> unitRepository, IBaseRepository<Domain.Entities.Recipe> recipesRepository, IBaseRepository<Domain.Entities.RecipeIngredient> recipeIngredientRepository , ILogger<CreateIngredientForRecipeHandler> logger, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _unitRepository = unitRepository;
            _recipesRepository = recipesRepository;
            _logger = logger;
            _recipeIngredientRepository = recipeIngredientRepository;
            _mapper = mapper;
        }

        public async Task<CreateIngredientForRecipeResponse> Handle(CreateIngredientForRecipeCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateIngredientForRecipeResponse();
            var validator = new CreateIngredientForRecipeValidator(_ingredientRepository, _recipesRepository, _unitRepository);

            try
            {
                var validationResult = await validator.ValidateAsync(request.RecipeIngredient, cancellationToken);

                if (validationResult.Errors.Any()) 
                { 
                    response.Success=false;
                    response.ValidationErrors = new();
                    foreach(var error in validationResult.Errors)
                    {
                        response.ValidationErrors.Add(error.ErrorMessage);
                    }

                    return response;    
                   
                }
                var entity = _mapper.Map<RecipeIngredient>(request.RecipeIngredient);
                var id = await _recipeIngredientRepository.AddAsync(entity);
                await _recipeIngredientRepository.SaveChangesAsync();
                _logger.LogInformation("Added ingredients to recipe {id}", entity.RecipeId);
                response.RecipeId = id.RecipeId;

                return response;

            }
            catch (Exception ex) 
            {
                throw;
            }
        }
    }
}
