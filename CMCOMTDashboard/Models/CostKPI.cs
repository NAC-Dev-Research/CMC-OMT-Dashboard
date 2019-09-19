using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMCOMTDashboard.Models
{
    public class CostKPI
    {
        public decimal TotalLimoWMT { get; set; }
        public decimal TotalSaproWMT { get; set; }

        public decimal JanuaryLimoWMT { get; set; }
        public decimal FebruaryLimoWMT { get; set; }
        public decimal MarchLimoWMT { get; set; }
        public decimal MayLimoWMT { get; set; }
        public decimal AprilLimoWMT { get; set; }
        public decimal JuneLimoWMT { get; set; }
        public decimal JulyLimoWMT { get; set; }
        public decimal AugustLimoWMT { get; set; }
        public decimal SeptemberLimoWMT { get; set; }
        public decimal OctoberLimoWMT { get; set; }
        public decimal NovemberLimoWMT { get; set; }
        public decimal DecemberLimoWMT { get; set; }

        public decimal JanuarySaproWMT { get; set; }
        public decimal FebruarySaproWMT { get; set; }
        public decimal MarchSaproWMT { get; set; }
        public decimal MaySaproWMT { get; set; }
        public decimal AprilSaproWMT { get; set; }
        public decimal JuneSaproWMT { get; set; }
        public decimal JulySaproWMT { get; set; }
        public decimal AugustSaproWMT { get; set; }
        public decimal SeptemberSaproWMT { get; set; }
        public decimal OctoberSaproWMT { get; set; }
        public decimal NovemberSaproWMT { get; set; }
        public decimal DecemberSaproWMT { get; set; }

        public decimal TotalBargeLimoWMT { get; set; }
        public decimal TotalBargeSaproWMT { get; set; }
        public decimal TotalManHour { get; set; }
        public decimal AverageDistance { get; set; }

        public CostKPI()
        {
            TotalLimoWMT = 0.0m;
            TotalSaproWMT = 0.0m;
            TotalBargeLimoWMT = 0.0m;
            TotalBargeSaproWMT = 0.0m;
            TotalManHour = 0.0m;
            AverageDistance = 0.0m;
        }
    }
}