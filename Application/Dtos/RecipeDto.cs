using Application.Features.Ingredient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RecipeDto
    {
        public string Title { get; set; }

        public int NoOfServings { get; set; }
        public int CookingTime { get; set; }
        public string Description { get; set; }

    }

    public class RecipeWithIngredientsDto : RecipeDto
    {
        public List<RecipeIngredientBaseDto> Ingredients { get; set; }

    }
}
