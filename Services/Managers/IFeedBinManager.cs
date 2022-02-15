using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Managers
{
    public interface IFeedBinManager
    {
        bool AddProduct(FeedBin feedBin, double volume);
        FeedBin CreateFeedBin(int binNumber, int currentVolume, string productName);
        void Flush(FeedBin feedBin);
        double RemoveProduct(FeedBin feedBin, double volume);
        void SetProductName(FeedBin feedBin, string newName);
    }
}
