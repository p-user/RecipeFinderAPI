using Application.Interfaces;
using MediatR;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Application.Features.Recipe.Validations;
using System.ComponentModel.DataAnnotations;
using Application.Dtos;

namespace Application.Features.Recipe.Queries.GetRecipes
{
    public class GetRecipesQueryHandler : IRequestHandler<GetRecipesQuery, GetRecipesResponse>
    {
        private readonly ILogger _logger;
        private readonly IBaseRepository<Domain.Entities.Recipe> _repository;
        private readonly IMapper _mapper;

        public GetRecipesQueryHandler(IBaseRepository<Domain.Entities.Recipe> repository, IMapper mapper, ILogger<GetRecipesQueryHandler> logger)
        {
             _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<GetRecipesResponse> Handle(GetRecipesQuery request, CancellationToken cancellationToken)
        {
            var response  = new GetRecipesResponse();
            var validator = new GetRecipesValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (validationResult.Errors.Count() > 0)
                {
                    response.Success = false;
                    response.ValidationErrors = new();
                    foreach (var error in validationResult.Errors)
                    {

                        //TODO:custoize error message
                        response.ValidationErrors.Add(error.ErrorMessage);
                    }
                }
                else if (response.Success)
                {
                    var result = await _repository.GetAllAsync();
                    response.Recipes = _mapper.Map<IReadOnlyList<RecipeDto>>(result);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
