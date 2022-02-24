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
    public class FeedBinManagerTests
    {
        private IFeedBinManager feedBinManager;

        public FeedBinManagerTests()
        {
            feedBinManager = new FeedBinManager();
        }

        [Fact]
        public void AddProduct_ShouldAddExpectAmount()
        {
            var feedBin = CreateFeedBin(20);

            var result = feedBinManager.AddProduct(feedBin,10);

            Assert.Equal(30, feedBin.CurrentVolume);
            Assert.True(result);
        }

        [Fact]
        public void AddProduct_ShouldNotAddAmount()
        {
            var feedBin = CreateFeedBin(20);

            var result = feedBinManager.AddProduct(feedBin, 30);

            Assert.Equal(20, feedBin.CurrentVolume);
            Assert.False(result);
        }

        [Fact]
        public void RemoveProduct_ShouldRemove()
        {
            var feedBin = CreateFeedBin(20);

            var result = feedBinManager.RemoveProduct(feedBin, 10);

            Assert.Equal(10, result);
            Assert.Equal(10, feedBin.CurrentVolume);
        }

        [Fact]
        public void RemoveProduct_ShouldRemoveLess()
        {
            var feedBin = CreateFeedBin(20);

            var result = feedBinManager.RemoveProduct(feedBin, 30);

            Assert.Equal(20, result);
            Assert.Equal(0, feedBin.CurrentVolume);
        }

        [Fact]
        public void FlushProduct_ShouldFlushFeedBin()
        {
            var feedBin = CreateFeedBin(20);

            feedBinManager.Flush(feedBin);

            Assert.Equal(0, feedBin.CurrentVolume);
        }

        [Fact]
        public void FlushProduct_ShouldFlushFeedBin_CurrentVolumeZero()
        {
            var feedBin = CreateFeedBin(0);

            feedBinManager.Flush(feedBin);

            Assert.Equal(0, feedBin.CurrentVolume);
        }

        [Fact]
        public void FlushProduct_ShouldSetName()
        {
            var feedBin = CreateFeedBin(0);

            feedBinManager.SetProductName(feedBin, "Fish");

            Assert.Equal("Fish", feedBin.ProductName);
        }

        [Fact]
        public void FlushProduct_ShouldNotSetName()
        {
            var feedBin = CreateFeedBin(10);

            feedBinManager.SetProductName(feedBin, "Fish");

            Assert.Equal("Meat", feedBin.ProductName);
        }

        private FeedBin CreateFeedBin(int currentVolume)
        {
            return new FeedBin
            {
                CurrentVolume = currentVolume,
                MaxVolume = 40,
                BinNumber = 1,
                ProductName = "Meat"
            };
        }
    }
}
