using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;


namespace Application.Users.UpdateUserDetailsCommand
{
    public class UpdateUserDetailsHandler(ILogger<UpdateUserDetailsHandler> logger,IUserContext userContext,IUserStore<ApplicationUser> userStore, IMapper _mapper)
            : IRequestHandler<UpdateUserDetailsCommand, UpdateUserDetailsResponse>
    {
        public async Task<UpdateUserDetailsResponse> Handle(UpdateUserDetailsCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateUserDetailsResponse();
            var user = userContext.GetCurrentUser();

            logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

            var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

            if (dbUser == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), user!.Id);
            }

            _mapper.Map(dbUser, request.UserDto);
            var result  =  await userStore.UpdateAsync(dbUser, cancellationToken);
            if (result.Succeeded)
            { 
                response.Success = true;
                response.Message = $"User details updated successfully";
               
            }
            else
            {
                response.Success = false;
                response.ValidationErrors = new();
                foreach (var error in result.Errors) 
                {
                    response.ValidationErrors.Add(error.Description);
                }
            }
            return response;
        }
    }
}
