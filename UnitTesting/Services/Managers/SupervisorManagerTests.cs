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
        public void test1()
        {
            var result = supervisorManager.CheckBatch(feedBins, Batch);

            Assert.True(result);
        }
    }
}
