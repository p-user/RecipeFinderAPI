using Application.Features.Ingredient.Queries.GetIngredientsQuery;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Validations
{
    public class GetIngredientsQueryValidator : AbstractValidator<GetIngredientsQuery>
    {
        public GetIngredientsQueryValidator()
        {
        }
    }
}
