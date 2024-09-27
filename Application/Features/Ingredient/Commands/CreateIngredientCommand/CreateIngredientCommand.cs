using Application.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Commands.CreateIngredientCommand
{
    public class CreateIngredientCommand :  IRequest<CreateIngredientResponse>
    {
        public IngredientDto Ingredient { get; set; }
    }
}
