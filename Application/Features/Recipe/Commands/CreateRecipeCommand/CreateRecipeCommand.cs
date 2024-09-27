using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Recipe.Commands.CreateRecipeCommand
{
    public class CreateRecipeCommand :  IRequest<CreateRecipeResponse>
    {
        public RecipeDto Recipe { get; set; }
    }
}
