using Application.Dtos;
using Application.Response;
using MediatR;

namespace Application.Users.AssignUserRole
{
    public class AssignUserRoleCommand :IRequest<AssignUserRoleResponse>
    {
       public UseRoleDto UserRoleDto { get; set; }
    }
}
