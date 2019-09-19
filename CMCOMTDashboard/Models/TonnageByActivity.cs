using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMCOMTDashboard.Models
{
    public class TonnageByActivity
    {
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int EquipmentID { get; set; }
        public Nullable<System.DateTime> MaterialDateOfEntry { get; set; }
        public Nullable<int> TripCount { get; set; }
        public Nullable<decimal> TonnageFactor { get; set; }
        public decimal Distance { get; set; }
        public decimal TonnageAmount { get; set; }

        public Nullable<int> TotalMinutes { get; set; }
        public string OriginLocation { get; set; }
        public string DestinationLocation { get; set; }
        public string MainActivity { get; set; }
        public string SpecificActivity { get; set; }
        public string GeneralActivity { get; set; }
        public string ActivityType { get; set; }
        
    }
}
