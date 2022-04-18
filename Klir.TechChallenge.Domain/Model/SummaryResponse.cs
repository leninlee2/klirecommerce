using System;
using System.Collections.Generic;
using System.Text;

namespace Klir.TechChallenge.Domain.Model
{
    public class SummaryResponse
    {
        public string productName { get; set; }
        public double Total { get; set; }

        public bool LastSummary { get; set; }
    }

}
