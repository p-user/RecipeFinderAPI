using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.AddApplicationUserCommand
{
    public class AddApplicatioUserCommand : IRequest<AddApplicatioUserResponse>
    {
        public UserRegistrationDto UserRegistrationDto {  get; set; }
    }
}
