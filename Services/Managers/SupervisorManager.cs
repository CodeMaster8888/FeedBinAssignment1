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
        private readonly IFeedBinManager _binManager;

        public SupervisorManager()
        {
            _binManager = new FeedBinManager();
        }

        public void MakeBatch(List<FeedBin> feedBins, Batch batch)
        {
            var (firstValue, secondValue) = CalculateValues(batch);

            var selectedBins = feedBins.Select(x => x)
                .Where(y => y.ProductName == batch.Recipe.FirstIngredient.ProductName ||
                y.ProductName == batch.Recipe.SecondIngredient.ProductName);

            foreach(var item in selectedBins)
            {
                if (item.ProductName == batch.Recipe.FirstIngredient.ProductName)
                {
                    _binManager.RemoveProduct(item, firstValue);
                }
                else
                {
                    _binManager.RemoveProduct(item, secondValue);
                }
            }
        }

        public List<ReportBatch> ReportBatch(List<FeedBin> feedBins, Batch batch)
        {
            List<ReportBatch> reportBatches = new List<ReportBatch>();

            var (firstValue, secondValue) = CalculateValues(batch);

            FeedBin? firstCheck = feedBins.SingleOrDefault(x => x.ProductName == batch.Recipe.FirstIngredient.ProductName);
            FeedBin? secondCheck = feedBins.SingleOrDefault(x => x.ProductName == batch.Recipe.SecondIngredient.ProductName);

            if (firstCheck == null)
            {
                reportBatches.Add(new ReportBatch
                {
                    Id = 1,
                    Reason = "First Ingredient " + batch.Recipe.FirstIngredient.ProductName + " not in any of the feed bins"
                });
            }
            else if (firstCheck.CurrentVolume < firstValue)
            {
                reportBatches.Add(new ReportBatch
                {
                    Id = 3,
                    FeedBin = firstCheck,
                    Reason = "There is not enough product in the feed bin"
                });
            }
            if (secondCheck == null)
            {
                reportBatches.Add(new ReportBatch
                {
                    Id = 2,
                    Reason = "Second Ingredient " + batch.Recipe.SecondIngredient.ProductName + " not in any of the feed bins"
                });
            }
            else if (secondCheck.CurrentVolume < secondValue)
            {
                reportBatches.Add(new ReportBatch
                {
                    Id = 4,
                    FeedBin = secondCheck,
                    Reason = "There is not enough product in the feed bin"
                });
            }

            return reportBatches;
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
