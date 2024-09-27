using Application.Dtos;
using Application.Features.Ingredient.Validations;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Recipe.Queries.RecipeWithIngredientQuery
{
    public class GetRecipeWithIngredientQueryHandler : IRequestHandler<GetRecipeWithIngredientQuery, GetRecipeWithIngredientQueryResponse>
    {
        private readonly IRecipeRepository _recipeRepository;   
        private readonly IMapper _mapper;
        private ILogger<GetRecipeWithIngredientQueryHandler> _logger;
        public GetRecipeWithIngredientQueryHandler(IRecipeRepository recipeRepository, IMapper mapper, ILogger<GetRecipeWithIngredientQueryHandler> logger)
        {
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }
        public async Task<GetRecipeWithIngredientQueryResponse> Handle(GetRecipeWithIngredientQuery request, CancellationToken cancellationToken)
        {
            var result = new GetRecipeWithIngredientQueryResponse();
            var validator = new GetRecipeWithIngredientQueryValidator();

            try
            {
                var validationResponse = await validator.ValidateAsync(request, cancellationToken);
                if (validationResponse.Errors.Any())
                {
                    result.Success = false;
                    result.ValidationErrors = new();
                    foreach (var error in validationResponse.Errors)
                    {
                        result.ValidationErrors.Add(error.ErrorMessage);
                    }
                }

                var entity = await _recipeRepository.GetRecipeWithIngredients(request.Id);
                result.Recipe = _mapper.Map < RecipeWithIngredientsDto >(entity);
                return result;

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ValidationErrors = new();
                result.ValidationErrors.Add(ex.Message);
                return result;
            }
        }
    }
}
