using Application.Dtos;
using Application.Features.Ingredient.Validations;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Ingredient.Queries.GetIngredientQuery
{
    public class GetIngredientQueryHandler : IRequestHandler<GetIngredientQuery, GetIngredientQueryResponse>
    {
        private readonly IBaseRepository<Domain.Entities.Ingredient> baseRepository;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public GetIngredientQueryHandler(IBaseRepository<Domain.Entities.Ingredient> baseRepository, ILogger<GetIngredientQueryHandler> logger, IMapper mapper)
        {
            this.baseRepository = baseRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<GetIngredientQueryResponse> Handle(GetIngredientQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Ingredient get reuest with id :{id}", request.Id);
            var response = new GetIngredientQueryResponse();
            var validator = new GetIngredientQueryValidator();
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
                else if (response.Success)
                {
                    var entity = await baseRepository.GetAsync(request.Id);
                    var mapped = mapper.Map<IngredientDto>(entity);
                    response.Ingredient = mapped;
                }
                return response;
            }
            catch (Exception ex) { throw; }
        }
    }
}
