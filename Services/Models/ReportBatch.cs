using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class ReportBatch
    {
        public int Id { get; set; }
        public FeedBin FeedBin { get; set; }
        public string Reason { get; set; }
    }
}
