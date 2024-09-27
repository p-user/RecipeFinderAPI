using Application.Dtos;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Recipe.Queries.RecipeWithIngredientQuery
{
    public class GetRecipeWithIngredientQueryResponse : BaseResponse
    {
        public RecipeWithIngredientsDto Recipe { get; set; }
    }
}
