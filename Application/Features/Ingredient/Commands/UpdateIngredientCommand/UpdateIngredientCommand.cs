using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Commands.UpdateIngredientCommand
{
    public class UpdateIngredientCommand : BaseCommandQuery, IRequest<UpdateIngredientResponse>
    {
        public IngredientDto Ingredient { get; set; }
    }
}
