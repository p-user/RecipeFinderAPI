using Application.Features.Recipe.Queries.GetRecipes;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Recipe.Validations
{
    public class GetRecipesValidator : AbstractValidator<GetRecipesQuery>
    {
        public GetRecipesValidator()
        {
        }
    }
}
