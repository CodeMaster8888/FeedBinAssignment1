using Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Models
{
    public class Batch
    {
        public int Id { get; set; }
        public Recipe Recipe { get; set; }
        public double Amount { get; set; }
    }
}
