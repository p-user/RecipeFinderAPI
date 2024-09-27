using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RecipeIngredient : BaseEntity
    {

        public Guid RecipeId { get; set; }
        [ForeignKey(nameof(RecipeId))]
        public Recipe Recipe { get; set; }

        public Guid IngredientId { get; set; }
        [ForeignKey(nameof(IngredientId))]
        public Ingredient Ingredient { get; set; }

        public double Quantity { get; set; }
        public Guid UnitId { get; set; }
        [ForeignKey(nameof(UnitId))]
        public MeasurementUnit Unit { get; set; }
    }
}
