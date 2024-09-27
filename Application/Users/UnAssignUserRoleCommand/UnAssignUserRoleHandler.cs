using Domain.Entities;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.UnAssignUserRoleCommand
{
    public class UnAssignUserRoleHandler(ILogger<UnAssignUserRoleHandler> _logger, UserManager<ApplicationUser> _userManager,RoleManager<IdentityRole> _roleManager)
        : IRequestHandler<UnAssignUserRoleCommand, UnAssignUserRoleResponse>
    {
        public async  Task<UnAssignUserRoleResponse> Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            var response = new UnAssignUserRoleResponse();
            _logger.LogInformation("Unassigning user role: {@Request}", request);

            var user = await _userManager.FindByEmailAsync(request.UserRoleDto.UserEmail)
                ?? throw new NotFoundException(nameof(ApplicationUser), request.UserRoleDto.UserEmail);

            var role = await _roleManager.FindByNameAsync(request.UserRoleDto.RoleName)
                ?? throw new NotFoundException(nameof(IdentityRole), request.UserRoleDto.RoleName);

           var result =  await _userManager.RemoveFromRoleAsync(user, role.Name!);
            if (result.Succeeded)
            {
                response.Success = true;
                response.Message = $"User's role unassigned successfully!";
            }
            else
            {
                response.Success= false;
                response.ValidationErrors = new();
                foreach(var error in result.Errors)
                {
                    response.ValidationErrors.Add(error.Description);
                }
            }

            return response;
        }
    }
}
