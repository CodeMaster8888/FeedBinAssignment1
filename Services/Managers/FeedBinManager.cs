using Services.Models;

namespace Services.Managers
{
    public class FeedBinManager : IFeedBinManager
    {
        public List<FeedBin> FeedBinList { get; set; }

        public FeedBinManager()
        {
            FeedBinList = new List<FeedBin>
            {
                new FeedBin{
                    BinNumber = 1,
                    CurrentVolume = 0,
                    MaxVolume = 40,
                    ProductName = ""
                },
                new FeedBin{
                    BinNumber = 2,
                    CurrentVolume = 0,
                    MaxVolume = 40,
                    ProductName = ""
                },
                new FeedBin{
                    BinNumber = 3,
                    CurrentVolume = 0,
                    MaxVolume = 40,
                    ProductName = ""
                }
            };

        }

        public void SetProductName(FeedBin feedBin, string newName)
        {
            if (feedBin.CurrentVolume == 0.0)
            {
                feedBin.ProductName = newName;
            }
        }

        public void Flush(FeedBin feedBin)
        {
            feedBin.CurrentVolume = 0.0;
        }

        public bool AddProduct(FeedBin feedBin, double volume)
        {
            if (feedBin.MaxVolume >= feedBin.CurrentVolume + volume)
            {
                feedBin.CurrentVolume += volume;
                return true;
            }
            else
                return false;
        }

        public double RemoveProduct(FeedBin feedBin, double volume)
        {
            if (feedBin.CurrentVolume >= volume)
            {
                feedBin.CurrentVolume -= volume;
            }
            else
            {
                volume = feedBin.CurrentVolume;
                feedBin.CurrentVolume = 0.0;
            }
            return volume;  // actual amount removed - may be less than requested  
        }
    }
}