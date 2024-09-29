using Application.Constants;
using Application.Users.AssignUserRole;
using Application.Users.Validators;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.AddApplicationUserCommand
{
    public class AddApplicationUserHandler(ILogger<AssignUserRoleHandler> _logger, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager) 
        : IRequestHandler<AddApplicatioUserCommand, AddApplicatioUserResponse>
    {
        public async Task<AddApplicatioUserResponse> Handle(AddApplicatioUserCommand request, CancellationToken cancellationToken)
        {
            var response = new AddApplicatioUserResponse();
            var validator = new AddApplicationUserValidator(_userManager, _roleManager);
            try
            {
                var validationResult =  await validator.ValidateAsync(request.UserRegistrationDto, cancellationToken);
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
                    var newUser = new ApplicationUser
                    {
                        Email = request.UserRegistrationDto.Email,
                        UserName = request.UserRegistrationDto.Name,

                    };

                    var isCreated = await _userManager.CreateAsync(newUser, request.UserRegistrationDto.Password);

                    if (isCreated.Succeeded)
                    {

                        var entity = await _userManager.AddToRoleAsync(newUser, GlobalConstants.RoleUser);
                        _logger.LogInformation($"User with email {request.UserRegistrationDto.Email}, is registerd in database!");
                        response.Message = $"User {request.UserRegistrationDto.Email} was created successfully! Please log in!";

                    }
                    else
                    {
                        response.Success = false;
                        response.ValidationErrors = new();
                        foreach (var error in isCreated.Errors)
                        {
                            response.ValidationErrors.Add(error.Description);
                        }
                    }

                }
               
            }
            catch (Exception ex) 
            { 
                response.Success = false;
                response.ValidationErrors = new();
                response.ValidationErrors.Add(ex.Message);

            }
            return response;
        }
    }
}
