using Application.Features.Ingredient.Queries.GetIngredientQuery;
using Application.Features.Recipe.Validations;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Recipe.Commands.CreateRecipeCommand
{
    public class CreateRecipeHandler : IRequestHandler<CreateRecipeCommand, CreateRecipeResponse>
    {
        private readonly IBaseRepository<Domain.Entities.Recipe> _baseRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public CreateRecipeHandler(IBaseRepository<Domain.Entities.Recipe> baseRepository, ILogger<CreateRecipeHandler> logger, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CreateRecipeResponse> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            var response =  new CreateRecipeResponse();
            var validation = new CreateRecipeValidator();

       

            try
            {
                var validationResult = await  validation.ValidateAsync(request, cancellationToken);
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
                    var entity = _mapper.Map<Domain.Entities.Recipe>(request.Recipe);
                    var result = await _baseRepository.AddAsync(entity);
                    await _baseRepository.SaveChangesAsync();
                    _logger.LogInformation("recipe with {id} created!", entity.Id);
                    response.Id = result.Id;
                }
                return response;
            }
            catch (Exception ex) { throw; }
        }
    }
}
