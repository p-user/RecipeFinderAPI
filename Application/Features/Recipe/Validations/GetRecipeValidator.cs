using Application.Features.Recipe.Queries.GetRecipe;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Recipe.Validations
{
    public class GetRecipeValidator : AbstractValidator<GetRecipeQuery>
    {
        public GetRecipeValidator()
        {
        }
    }
}
