using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Managers
{
    public class SupervisorManager : ISupervisorManager
    {
        public SupervisorManager()
        {

        }

        public void MakeBatch()
        {

        }

        public void ReportBatch()
        {
           
        }

        public bool CheckBatch(List<FeedBin> feedBins, Batch batch)
        {
            var (firstValue, secondValue) = CalculateValues(batch);


            var temp = feedBins.Select(x => {
                
                if(x.ProductName == batch.Recipe.FirstIngredient.ProductName) 
                {
                    if (CanRemove(x, firstValue))
                    {
                        return true;
                    }
                }else if (x.ProductName == batch.Recipe.SecondIngredient.ProductName)
                {
                    if (CanRemove(x, firstValue))
                    {
                        return true;
                    }
                }
                return false;
            });

            var count =  temp.Count(x => x == true);

            return count == 2;
        }

        private (double, double) CalculateValues(Batch batch)
        {
            var firstPercentage = batch.Recipe.FirstPercentage;
            var secondPercentage = batch.Recipe.SecondPercentage;

            var firstValueTaken = firstPercentage * batch.Amount;
            var secondValueTaken = secondPercentage * batch.Amount;

            return (firstValueTaken, secondValueTaken);
        }

        private bool CanRemove(FeedBin feedBin, double valueToRemove)
        {
            if(feedBin.CurrentVolume >= valueToRemove)
            {
                return true;
            }
            return false;
        }
    }
}
