using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Commands.CreateIngredientCommand
{
    public class CreateIngredientResponse : BaseResponse
    {
        public Guid Id { get; set; }
    }
}
