﻿using Application.Constants;
using Application.Dtos;
using Application.Users.AssignUserRole;
using Application.Users.UnAssignUserRoleCommand;
using Application.Users.UpdateUserDetailsCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeFinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BasicController
    {
        [HttpPatch("user")]
        [Authorize]
        public async Task<ActionResult> UpdateUserDetails([FromBody]ApplicationUserDto applicationUserDto)
        {
            var result = await Mediator.Send(new UpdateUserDetailsCommand
            { 
                UserDto = applicationUserDto
            });
            return Ok(result);
        }


        [HttpPost("userRole")]
        [Authorize(Roles = GlobalConstants.AdminUser)]
        public async Task<ActionResult> AssignUserRole([FromBody] UseRoleDto applicationUserDto)
        {
            var result  = await Mediator.Send(new AssignUserRoleCommand
            {
                UserRoleDto = applicationUserDto,
            });
            return Ok(result);
        }

        [HttpDelete("userRole")]
        [Authorize(Roles = GlobalConstants.AdminUser)]
        public async Task<ActionResult> UnassignUserRole([FromBody] UseRoleDto command)
        {
            var result = await Mediator.Send(new UnAssignUserRoleCommand
            {
                UserRoleDto = command
            });
            return NoContent();
        }
    }
}
