using Application.Features.Ingredient.Commands.DeleteIngredientForRecipeCommand;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Commands.DeleteIngredientCommand
{
    public class DeleteIngredientCommand : IRequest<DeleteIngredientResponse>
    {
        public Guid Id { get; set; }
    }
}
