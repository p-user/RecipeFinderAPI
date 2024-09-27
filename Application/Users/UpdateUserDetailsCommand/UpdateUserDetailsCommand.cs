using Application.Dtos;
using MediatR;


namespace Application.Users.UpdateUserDetailsCommand
{
    public class UpdateUserDetailsCommand : IRequest<UpdateUserDetailsResponse>
    {
        public ApplicationUserDto UserDto { get; set; }
    }
}
