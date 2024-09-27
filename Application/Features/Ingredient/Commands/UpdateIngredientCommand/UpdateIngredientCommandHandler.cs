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

namespace Application.Features.Ingredient.Commands.UpdateIngredientCommand
{
    public class UpdateIngredientCommandHandler : IRequestHandler<UpdateIngredientCommand, UpdateIngredientResponse>
    {
        private readonly IBaseRepository<Domain.Entities.Ingredient> baseRepository;
        private readonly ILogger<UpdateIngredientCommandHandler> logger;
        private readonly IMapper mapper;

        public UpdateIngredientCommandHandler(IBaseRepository<Domain.Entities.Ingredient> baseRepository, ILogger<UpdateIngredientCommandHandler> logger, IMapper mapper)
        {
            this.baseRepository = baseRepository;
            this.logger = logger;
            this.mapper = mapper;
        }

        public async Task<UpdateIngredientResponse> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateIngredientResponse();
            var validator = new UpdateIngredientCommandValidator(baseRepository);
            try
            {
                var validationResult  = await validator.ValidateAsync(request, cancellationToken);
                if (validationResult.Errors.Any())
                {
                    response.Success = false;
                    response.ValidationErrors = new();
                    foreach(var error in validationResult.Errors)
                    {
                        response.ValidationErrors.Add(error.ErrorMessage);
                    }
                }
                else
                {
                    var entity = await baseRepository.GetAsync(request.Id);
                    mapper.Map(entity, request.Ingredient);
                    baseRepository.UpdateAsync(entity);
                    await baseRepository.SaveChangesAsync();
                    response.Id = entity.Id;
                }

                return response;

            }
            catch (Exception ex) { throw; }
        }
    }
}
