using Application.Features.Ingredient.Validations;
using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Commands.CreateIngredientCommand
{
    public class CreateIngredientHandler : IRequestHandler<CreateIngredientCommand, CreateIngredientResponse>
    {

        private readonly IMapper _mapper;
        private readonly IBaseRepository<Domain.Entities.Ingredient> _baseRepository;
        private readonly IBaseRepository<Domain.Entities.RecipeIngredient> _intermediateRepository;
        private readonly ILogger _logger;

        public CreateIngredientHandler(IMapper mapper, IBaseRepository<Domain.Entities.Ingredient> baseRepository, ILogger<CreateIngredientHandler> logger, IBaseRepository<Domain.Entities.RecipeIngredient> intermediateRepository)
        {
            _mapper = mapper;
            _baseRepository = baseRepository;
            _logger = logger;
            _intermediateRepository = intermediateRepository;
        }

        public async  Task<CreateIngredientResponse> Handle(CreateIngredientCommand request, CancellationToken cancellationToken)
        {
           var response = new CreateIngredientResponse();
            var validation = new CreateIngredientValidator();

            try
            {
                var validationResult = await validation.ValidateAsync(request.Ingredient, cancellationToken);
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
                    var entity = _mapper.Map<Domain.Entities.Ingredient>(request.Ingredient);
                    var subEntity = _mapper.Map<Domain.Entities.RecipeIngredient>(request.Ingredient);
                     await _baseRepository.AddAsync(entity);
                    var result = await _intermediateRepository.AddAsync(subEntity);

                    await _baseRepository.SaveChangesAsync();
                   //await _intermediateRepository.SaveChangesAsync();
                    response.Id = result.Id;
                }

                return response;    
            }

            catch (Exception ex) {
                throw;
            }
        }
    }
}
