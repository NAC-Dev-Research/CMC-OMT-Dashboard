using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMCOMTDashboard.Models
{
    public class Tonnage
    {
        public int ID { get; set; }
        public string MaterialName { get; set; }
        public Nullable<System.DateTime> MaterialDateOfEntry { get; set; }
        public Nullable<int> TripCount { get; set; }
        public Nullable<decimal> TonnageFactor { get; set; }
        public decimal Distance { get; set; }
        public decimal TonnageAmount { get; set; }

        public decimal TonnageDaily { get; set; }
        public decimal TonnageYearly { get; set; }
        public decimal TonnageMonthly { get; set; }
        public decimal TonnageOneMonthBefore { get; set; }
        public decimal TonnageTwoMonthsBefore { get; set; }
        public decimal DailyTonnageWaste { get; set; }
        public decimal DailyTonnageLimonite { get; set; }
        public decimal DailyTonnageSaprolite { get; set; }
        public decimal MonthlyTonnageWaste { get; set; }
        public decimal MonthlyTonnageLimonite { get; set; }
        public decimal MonthlyTonnageSaprolite { get; set; }
        public decimal YearlyTonnageWaste { get; set; }
        public decimal YearlyTonnageLimonite { get; set; }
        public decimal YearlyTonnageSaprolite { get; set; }

        public string DateDaily { get; set; }
        public string DateYearly { get; set; }
        public string DateMonthly { get; set; }
        public string DateOneMonthBefore { get; set; }
        public string DateTwoMonthsBefore { get; set; }
        
    }
}