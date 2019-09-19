using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMCOMTDashboard.Models
{
    public class MajorActivityDetails
    {
        public decimal TotalWMT { get; set; }
        public decimal TotalHours { get; set; }
        public decimal TotalWMTPerHour { get; set; }
        public decimal LimoWMT { get; set; }
        public decimal LimoHours { get; set; }
        public decimal SaproWMT { get; set; }
        public decimal SaproHours { get; set; }
        public decimal WasteWMT { get; set; }
        public decimal WasteHours { get; set; }

        public MajorActivityDetails()
        {
            TotalWMT = 0.0m;
            TotalHours = 0.0m;
            TotalWMTPerHour = 0.0m;
            LimoWMT = 0.0m;
            LimoHours = 0.0m;
            SaproWMT = 0.0m;
            SaproHours = 0.0m;
            WasteWMT = 0.0m;
            WasteHours = 0.0m;
        }
    }
}