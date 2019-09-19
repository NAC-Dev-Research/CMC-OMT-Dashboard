using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMCOMTDashboard.DAL;
using CMCOMTDashboard.Models;
using CMCOMTDashboard.Models.Cost;

namespace CMCOMTDashboard.Controllers
{
    public class CostController : ComputeCostController
    {
        CMCOMTDashboardEntities _cmcOmtSqlDb;
        List<TonnageByActivity> finalDEURList = new List<TonnageByActivity>();
        public CostController()
        {
            _cmcOmtSqlDb = new CMCOMTDashboardEntities();
        }

        // GET: Cost
        public ActionResult Setting()
        {
            if (Session["user"] == null)
                Response.Redirect("/Reports/Login", true);

            int currentMonth = System.DateTime.Now.Month;
            CostSetting curCostSetting = _cmcOmtSqlDb.CostSettings.Where(s => s.MonthID == currentMonth).FirstOrDefault();

            return View(curCostSetting);
        }
        [HttpPost]
        public ActionResult Setting(int? month, string productionCost, string bargeCost, string totalSiteCost)
        {
            if (Session["user"] == null)
                Response.Redirect("/Reports/Login", true);
            
            CostSetting updatedSetting = _cmcOmtSqlDb.CostSettings.Where(s => s.MonthID == month).FirstOrDefault(); ;

            if (updatedSetting != null)
            {
                System.Diagnostics.Debug.WriteLine("Raw: " + productionCost + " | Converted: " + Convert.ToDecimal(productionCost.Replace(",", "")));
                updatedSetting.ProductionCost = Convert.ToDecimal(productionCost.Replace(",", ""));
                updatedSetting.BargeloadingCost = Convert.ToDecimal(bargeCost.Replace(",", ""));
                updatedSetting.TotalSiteCost = Convert.ToDecimal(totalSiteCost.Replace(",", ""));

                _cmcOmtSqlDb.Entry(updatedSetting).State = System.Data.Entity.EntityState.Modified;
                _cmcOmtSqlDb.SaveChanges();
            }

            return View(updatedSetting);
        }

        public ActionResult CostSummary()
        {
            return View();
        }

        public ActionResult CostPerKPI()
        {
            if (Session["user"] == null)
                Response.Redirect("/Reports/Login", true);

            finalDEURList = (List<TonnageByActivity>)Session["DEURList"];
            if (finalDEURList == null)
                finalDEURList = GenerateDEURList(1);

            CostKPI costKPIVariables = new CostKPI();
            costKPIVariables = ComputeCostVariables(finalDEURList);

            ProdCostPerOreMinedDetails costDetails = new ProdCostPerOreMinedDetails();
            costDetails = ComputeProdCostPerOReMined(costKPIVariables);
            Session["ProdCostPerOreMined"] = costDetails;

            return View(costDetails);
        }
    }
}
