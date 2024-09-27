using Application.Features.Ingredient.Queries.GetIngredientQuery;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Validations
{
    public class GetIngredientQueryValidator : AbstractValidator<GetIngredientQuery>
    {
        public GetIngredientQueryValidator()
        {
        }
    }
}
