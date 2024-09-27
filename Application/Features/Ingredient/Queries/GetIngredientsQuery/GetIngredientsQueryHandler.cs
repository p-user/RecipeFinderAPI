using Application.Dtos;
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

namespace Application.Features.Ingredient.Queries.GetIngredientsQuery
{
    public class GetIngredientsQueryHandler : IRequestHandler<GetIngredientsQuery, GetIngredientsQueryResponse>
    {
        private readonly IBaseRepository<Domain.Entities.Ingredient> _baseRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public GetIngredientsQueryHandler(IBaseRepository<Domain.Entities.Ingredient> baseRepository, IMapper mapper, ILogger<GetIngredientsQueryHandler> logger)
        {
            _baseRepository = baseRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<GetIngredientsQueryResponse> Handle(GetIngredientsQuery request, CancellationToken cancellationToken)
        {
            var response  = new GetIngredientsQueryResponse();
            var validator = new GetIngredientsQueryValidator();

            try
            {
                var validationResult = await validator.ValidateAsync(request);
                if (validationResult.Errors.Any())
                {
                    response.Success = false;
                    response.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors)
                    {
                        response.ValidationErrors.Add(error.ErrorMessage);
                    }
                }
                else
                {
                    response.Success = true;
                    var entities = await _baseRepository.GetAllAsync();
                    var mapped = mapper.Map<List<IngredientDto>>(entities);
                    response.Ingredients = mapped;


                }
                return response;
            }
            catch (Exception ex) { throw; }
        }
    }
}
