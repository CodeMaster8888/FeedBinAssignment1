using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Ingredient FirstIngredient { get; set; }
        public Ingredient SecondIngredient { get; set; }
        public double FirstPercentage { get; set; }
        public double SecondPercentage { get; set; }
    }
}
