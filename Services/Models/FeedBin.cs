using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class FeedBin
    {
        public int BinNumber { get; set; }
        public string ProductName { get; set; }
        public double MaxVolume { get; set; }
        public double CurrentVolume { get; set; }
    }
}
