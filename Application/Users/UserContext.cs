﻿using Application.Constants;
using Application.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Extensions
{

    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
    public class UserContext(IHttpContextAccessor _httpContextAccessor) : IUserContext
    {
       
        public CurrentUser? GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null)
            {
                throw new InvalidOperationException("User context is not present");
            }

            if (user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
            var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
            var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
            var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
            var dateOfBirthString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
            var dateOfBirth = dateOfBirthString == null
                ? (DateOnly?)null
                : DateOnly.ParseExact(dateOfBirthString, "dd-MM-yyyy");

            return new CurrentUser(userId, email, roles, dateOfBirthString, dateOfBirth);
        }
    }
    
}
