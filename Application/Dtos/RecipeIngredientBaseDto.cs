using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class RecipeIngredientBaseDto
    {
        public Guid IngredientId { get; set; }
        public Guid RecipeId { get; set; }
        public double Quantity { get; set; }
        public Guid UnitId { get; set; }

    }


    
}
