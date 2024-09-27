using Application.Dtos;
using Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Ingredient.Queries.GetIngredientsQuery
{
    public class GetIngredientsQueryResponse : BaseResponse
    {
        public List<IngredientDto> Ingredients { get; set; }
    }
}
