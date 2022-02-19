using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Context
{
    public class IngredientContext : IIngredientContext
    {
        private FeedBinContext feedBinContext;

        public IngredientContext(FeedBinContext feedBinContext)
        {
            this.feedBinContext = feedBinContext ?? throw new ArgumentNullException(nameof(feedBinContext));
        }

        public List<Ingredient> GetIngredients()
        {
            return feedBinContext.Ingredients.Select(x => x).ToList();
        }
    }
}
