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
    public class CostHttpGetController : Controller
    {
        CMCOMTDashboardEntities _cmcOmtSqlDb;
        List<TonnageByActivity> finalDEURList = new List<TonnageByActivity>();
        public CostHttpGetController()
        {
            _cmcOmtSqlDb = new CMCOMTDashboardEntities();
        }

        [HttpGet]
        public JsonResult GetCostSetting(int chosenMonth)
        {
            _cmcOmtSqlDb.Configuration.ProxyCreationEnabled = false;
            CostSetting costSetting = _cmcOmtSqlDb.CostSettings.Where(s => s.MonthID == chosenMonth).FirstOrDefault();

            return Json(costSetting, JsonRequestBehavior.AllowGet);
        }
    }
}