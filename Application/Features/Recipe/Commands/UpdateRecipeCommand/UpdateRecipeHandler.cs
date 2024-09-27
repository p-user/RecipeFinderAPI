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

namespace Application.Features.Recipe.Commands.UpdateRecipeCommand
{
    public class UpdateRecipeHandler : IRequestHandler<UpdateRecipeCommand, UpdateRecipeCommandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Domain.Entities.Recipe> _repository;
        private readonly ILogger _logger;

        public UpdateRecipeHandler(IMapper mapper, IBaseRepository<Domain.Entities.Recipe> repository, ILogger<UpdateRecipeHandler> logger)
        {
            _mapper = mapper;
            _repository = repository;
            _logger = logger;
        }

        public async Task<UpdateRecipeCommandResponse> Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateRecipeCommandResponse();
            var validation = new UpdateRecipeValidator(_repository);

            try
            {

                var validationResult  = await validation.ValidateAsync(request, cancellationToken);
                if (validationResult.Errors.Any())
                {
                    response.Success = false;
                    response.ValidationErrors = new List<string>();
                    foreach (var error in validationResult.Errors) 
                    {
                        response.ValidationErrors.Add(error.ErrorMessage);
                    }

                }
                else if(response.Success)
                { 
                    var entity = await _repository.GetAsync(response.Id);
                    _logger.LogInformation("recipe with {id} before update {entity}!", entity.Id , entity);
                    _mapper.Map(entity, request.Recipe);
                    _repository.UpdateAsync(entity);
                    await _repository.SaveChangesAsync();
                    _logger.LogInformation("recipe with {id} successfully updated!Last state : {entity}", entity.Id , entity);
                }

                return response;

            }
            catch (Exception ex) { throw; }
        }
    }
}
