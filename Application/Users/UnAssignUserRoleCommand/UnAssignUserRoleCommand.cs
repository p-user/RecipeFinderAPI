using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.UnAssignUserRoleCommand
{
    public class UnAssignUserRoleCommand : IRequest<UnAssignUserRoleResponse>
    {
        public UserRoleDto UserRoleDto {  get; set; }
    }
}
