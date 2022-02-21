using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Managers
{
    public interface ISupervisorManager
    {
        bool CheckBatch(List<FeedBin> feedBins, Batch batch);
        void MakeBatch(List<FeedBin> feedBins, Batch batch);
        List<ReportBatch> ReportBatch(List<FeedBin> feedBins, Batch batch);
    }
}
