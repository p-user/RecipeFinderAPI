using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MeasurementUnit : BaseEntity
    {
        public string? Unit { get; set; }
        public string Symbol { get; set; }
        public virtual ICollection<RecipeIngredient> Ingredients { get; set; }
    }
}
