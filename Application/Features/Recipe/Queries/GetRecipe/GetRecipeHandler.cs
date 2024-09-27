using Application.Dtos;
using Application.Features.Recipe.Validations;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Recipe.Queries.GetRecipe
{
    public class GetRecipeHandler : IRequestHandler<GetRecipeQuery, GetRecipeResponse>
    {
        private readonly ILogger _logger;
        private readonly IBaseRepository<Domain.Entities.Recipe> _repository;
        private readonly IMapper _mapper;

        public GetRecipeHandler(ILogger<GetRecipeHandler> logger, IBaseRepository<Domain.Entities.Recipe> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<GetRecipeResponse> Handle(GetRecipeQuery request, CancellationToken cancellationToken)
        {
            var response = new GetRecipeResponse();
            var validator = new GetRecipeValidator();
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
                    var result = await _repository.GetAsync(request.Id);
                    response.Recipe = _mapper.Map<RecipeDto>(result);
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
