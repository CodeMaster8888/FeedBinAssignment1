using Database.Models;
using Services.Managers;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTesting.Services.Managers
{
    public class SupervisorManagerTests
    {
        ISupervisorManager supervisorManager = new SupervisorManager();
        List<FeedBin> feedBins;
        Batch Batch;

        public SupervisorManagerTests()
        {
            feedBins = new List<FeedBin>
            {
                new FeedBin
                {
                    BinNumber = 1,
                    CurrentVolume = 20,
                    MaxVolume = 40,
                    ProductName = "Test1"
                },
                new FeedBin
                {
                    BinNumber = 2,
                    CurrentVolume = 20,
                    MaxVolume = 40,
                    ProductName = "Chicken"
                },
                new FeedBin
                {
                    BinNumber = 3,
                    CurrentVolume = 20,
                    MaxVolume = 40,
                    ProductName = "Fruit"
                }
            };
            Batch = new Batch
            {
                Amount = 20,
                Id = 1,
                Recipe = new Recipe
                {
                    Id = 1,
                    Name = "TestFood",
                    FirstIngredient = new Ingredient { Id = 1, ProductName = "Chicken"},
                    SecondIngredient = new Ingredient { Id = 2, ProductName = "Fruit" },
                    FirstPercentage = 0.7,
                    SecondPercentage = 0.3
                }
            };
        }

        [Fact]
        public void CheckBatch_ShouldBeTrue()
        {
            var result = supervisorManager.CheckBatch(feedBins, Batch);

            Assert.True(result);
        }

        [Fact]
        public void MakeBatch_ShouldRemoveVolume()
        {
            supervisorManager.MakeBatch(feedBins, Batch);

            Assert.Equal(
                6,
                feedBins.Single(x => x.ProductName == Batch.Recipe.FirstIngredient.ProductName).CurrentVolume);
            Assert.Equal(
                14,
                feedBins.Single(x => x.ProductName == Batch.Recipe.SecondIngredient.ProductName).CurrentVolume);
        }

        [Fact]
        public void ReportBatch_ShouldReportNoProductInBin()
        {
            var testBatch = new Batch
            {
                Amount = 40,
                Recipe = new Recipe
                {
                    FirstIngredient = new Ingredient
                    {
                         ProductName = "Test2"
                    },
                    SecondIngredient = new Ingredient
                    {
                        ProductName = "Test1"
                    },
                    FirstPercentage = 0.1,
                    SecondPercentage = 0.9
                }
            };

            var result = supervisorManager.ReportBatch(feedBins, testBatch);
            var expected = new List<ReportBatch>
            {
                new ReportBatch
                {
                    Id = 1,
                    Reason = "First Ingredient " +
                    testBatch.Recipe.FirstIngredient.ProductName + " not in any of the feed bins"
                },
                new ReportBatch
                {
                    Id = 4,
                    FeedBin = feedBins.Single(x => x.BinNumber == 1),
                    Reason = "There is not enough product in the feed bin"
                }
            };

            Assert.Equal(expected.Single(x => x.Id == 1).Reason, result.Single(x => x.Id == 1).Reason);
            Assert.Equal(expected.Single(x => x.Id == 4).FeedBin, result.Single(x => x.Id == 4).FeedBin);
        }
    }
}
