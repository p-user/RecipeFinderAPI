using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Recipe.Commands.CreateRecipeCommand
{
    public class CreateRecipeResponse : BaseResponse
    {
        public Guid Id { get; set; }
    }
}
