using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMCOMTDashboard.Models
{
    public class CostSetting
    {
        public int MonthID { get; set; }
        public string Month { get; set; }
        public decimal ProductionCost { get; set; }
        public decimal BargeloadingCost { get; set; }
        public decimal TotalSiteCost { get; set; }
    }
}