using Application.Constants;
using Application.Dtos;
using Application.Users.AddApplicationUserCommand;
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


        [HttpPost("AssignUserRole")]
        [Authorize(Roles = GlobalConstants.AdminUser)]
        public async Task<ActionResult> AssignUserRole([FromBody] UserRoleDto applicationUserDto)
        {
            var result  = await Mediator.Send(new AssignUserRoleCommand
            {
                UserRoleDto = applicationUserDto,
            });
            return Ok(result);
        }

        [HttpDelete("UnassignUserRole")]
        [Authorize(Roles = GlobalConstants.AdminUser)]
        public async Task<ActionResult> UnassignUserRole([FromBody] UserRoleDto command)
        {
            var result = await Mediator.Send(new UnAssignUserRoleCommand
            {
                UserRoleDto = command
            });
            return Ok(result);
        }


        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult> AddUser([FromBody] UserRegistrationDto applicationUserDto)
        {
            var result = await Mediator.Send(new AddApplicatioUserCommand
            {
                UserRegistrationDto = applicationUserDto,
            });
            return Ok(result);
        }
    }
}
