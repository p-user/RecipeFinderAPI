using Application.Features.Recipe.Queries.RecipeWithIngredientQuery;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Validations
{
    public class GetRecipeWithIngredientQueryValidator : AbstractValidator<GetRecipeWithIngredientQuery>
    {
        public GetRecipeWithIngredientQueryValidator()
        {

            RuleFor(s=>s.Id).NotEmpty()
                .NotNull();
        }
    }
}
