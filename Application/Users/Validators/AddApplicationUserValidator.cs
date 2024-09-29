using Application.Dtos;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Validators
{
    public class AddApplicationUserValidator : AbstractValidator<UserRegistrationDto>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public AddApplicationUserValidator(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> _roleManager)
        {
            _userManager = userManager;
            RuleFor(w => w.Email)
            .MustAsync(CheckIfUnique)
            .WithMessage("This email address is already registered!");

        }

        private async Task<bool> CheckIfUnique(string email, CancellationToken token)
        {
            var emailCheck = await _userManager.FindByEmailAsync(email);
            return emailCheck is  null;
        }
    }
}
