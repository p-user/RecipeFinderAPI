using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Serilog.Core;


namespace Application.Users.AssignUserRole
{
    public class AssignUserRoleHandler(ILogger<AssignUserRoleHandler> _logger, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
                                        : IRequestHandler<AssignUserRoleCommand, AssignUserRoleResponse>
    {
        public async Task<AssignUserRoleResponse> Handle(AssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            var response  = new AssignUserRoleResponse();
            _logger.LogInformation("Assigning user role: {@Request}", request);
            var user = await _userManager.FindByEmailAsync(request.UserRoleDto.UserEmail) ?? throw new NotFoundException(nameof(ApplicationUser), request.UserRoleDto.UserEmail);

            var role = await _roleManager.FindByNameAsync(request.UserRoleDto.RoleName)
                ?? throw new NotFoundException(nameof(IdentityRole), request.UserRoleDto.RoleName);

            var result = await _userManager.AddToRoleAsync(user, role.Name!);

            if (result.Succeeded) 
            {
                response.Success = true;
                response.Message = $"User assigned to role succesfully!";
                return response;
            }
            else
            {
                response.Success = false;
                response.ValidationErrors = new();
                foreach(var error in result.Errors)
                {
                    response.ValidationErrors.Add(error.Description);
                }
                return response;
            }
        }
    }
}
