using Application.Dtos;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Recipe.Queries.GetRecipe
{
    public class GetRecipeResponse : BaseResponse
    {

        public RecipeDto? Recipe { get; set; }
    }
}
