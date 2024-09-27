using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Recipe : BaseEntity

    {
        public string Title { get; set; }

        public int NoOfServings { get; set; }
        public int CookingTime { get; set; }
        //public TimeMeasure TimeMeasure { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public virtual ICollection<RecipeIngredient> Ingredients { get; set; }
        //public virtual ICollection<Review>? Reviews { get; set; }
        //public virtual ICollection<RecipeCategory>? recipeCategories { get; set; }
    }
}
