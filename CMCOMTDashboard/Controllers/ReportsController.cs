using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMCOMTDashboard.Models;
using CMCOMTDashboard.DAL;
using System.Threading.Tasks;

namespace CMCOMTDashboard.Controllers
{
    public class ReportsController : ComputeTonnageController
    {
        CMCOMTDashboardEntities _cmcOmtSqlDb;
        List<TonnageByActivity> finalDEURList = new List<TonnageByActivity>();
        public ReportsController()
        {
            _cmcOmtSqlDb = new CMCOMTDashboardEntities();
        }

        // GET: Reports
        public ActionResult Tonnage()
        {
            if (Session["user"] == null)
                Response.Redirect("Login", true);

            finalDEURList = (List<TonnageByActivity>)Session["DEURList"];
            if (finalDEURList == null)
                finalDEURList = GenerateDEURList(1);
            Tonnage curRunOfMineReport = GenerateRunOfMineReport(finalDEURList, System.DateTime.Now, false);
            if (curRunOfMineReport == null)
                return RedirectToAction("NoData", "Home");

            DateTime tempLatestDate = Convert.ToDateTime(curRunOfMineReport.DateDaily);
            string LatestReportDate = curRunOfMineReport.DateMonthly + "/" + tempLatestDate.Day + "/" + curRunOfMineReport.DateYearly;
            ViewBag.ReportDate = "Report for " + LatestReportDate;

            Session["ReportDate"] = LatestReportDate;
            Session["DEURList"] = finalDEURList;
            return View(curRunOfMineReport);
        }
        [HttpPost]
        public ActionResult Tonnage(DateTime? chosenDate)
        {
            if (Session["user"] == null)
                Response.Redirect("Login", true);

            if (chosenDate == null)
                return RedirectToAction("NoData", "Home");

            finalDEURList = (List<TonnageByActivity>)Session["DEURList"];
            Tonnage curRunOfMineReport = GenerateRunOfMineReport(finalDEURList, Convert.ToDateTime(chosenDate), true);
            if (curRunOfMineReport == null)
                return RedirectToAction("NoData", "Home");

            ViewBag.ReportDate = "Report for " + Convert.ToDateTime(chosenDate).ToString("MM/dd/yyyy");
            return View(curRunOfMineReport);
        }

        public ActionResult TonnageByActivity()
        {
            if (Session["user"] == null)
                Response.Redirect("Login", true);

            finalDEURList = (List<TonnageByActivity>)Session["DEURList"];
            if (finalDEURList == null)
                finalDEURList = GenerateDEURList(1);

            TonnageByMajorActivity curTonnageByMajorActivity = ComputeTonnageByActivity("PRODUCTION OPERATION", finalDEURList);
            if (curTonnageByMajorActivity == null)
                return RedirectToAction("NoData", "Home");

            ViewBag.Activity = "PRODUCTION";
            ViewBag.MajorActivity = "BARGELOADING/SHIPLOADING";
            ViewBag.BargeData = curTonnageByMajorActivity.BargeShipLoading.TotalWMT;
            ViewBag.Crushing = curTonnageByMajorActivity.Crushing.TotalWMT;
            ViewBag.DirectMining = curTonnageByMajorActivity.DirectMining.TotalWMT;
            ViewBag.EnviMajor = curTonnageByMajorActivity.EnviMajor.TotalWMT;
            ViewBag.EPEP = curTonnageByMajorActivity.EPEP.TotalWMT;
            ViewBag.MAMM = curTonnageByMajorActivity.MiningAreaMngmtMntnce.TotalWMT;
            ViewBag.PitPrep = curTonnageByMajorActivity.PitPrepMiscellaneousAct.TotalWMT;
            ViewBag.Rehandling = curTonnageByMajorActivity.Rehandling.TotalWMT;

            string LatestReportDate = (string)Session["ReportDate"];
            ViewBag.ReportDate = "Report for 1/1/" + LatestReportDate.Substring((LatestReportDate.Length - 4), 4) + " - " + LatestReportDate;

            Session["tonnageByActivitySession"] = curTonnageByMajorActivity;
            return View(curTonnageByMajorActivity.BargeShipLoading);
        }
        [HttpPost]
        public ActionResult TonnageByActivity(DateTime? chosenDateFrom, DateTime? chosenDateTo)
        {
            if (Session["user"] == null)
                Response.Redirect("Login", true);

            if (chosenDateFrom == null || chosenDateTo == null)
                return RedirectToAction("NoData", "Home");
            if (chosenDateFrom > chosenDateTo)
            {
                DateTime? temp = chosenDateFrom;
                chosenDateFrom = chosenDateTo;
                chosenDateTo = temp;
            }

            finalDEURList = (List<TonnageByActivity>)Session["DEURList"];
            List<TonnageByActivity> tempDEURList = finalDEURList.Where(s => s.MaterialDateOfEntry >= chosenDateFrom && s.MaterialDateOfEntry <= chosenDateTo).OrderByDescending(s => s.MaterialDateOfEntry).ToList();
            if (tempDEURList.Count == 0)
                return RedirectToAction("NoData", "Home");

            TonnageByMajorActivity curTonnageByMajorActivity = ComputeTonnageByActivity("PRODUCTION OPERATION", tempDEURList);
            if (curTonnageByMajorActivity == null)
                return RedirectToAction("NoData", "Home");

            ViewBag.Activity = "PRODUCTION";
            ViewBag.MajorActivity = "BARGELOADING/SHIPLOADING";
            ViewBag.BargeData = curTonnageByMajorActivity.BargeShipLoading.TotalWMT;
            ViewBag.Crushing = curTonnageByMajorActivity.Crushing.TotalWMT;
            ViewBag.DirectMining = curTonnageByMajorActivity.DirectMining.TotalWMT;
            ViewBag.EnviMajor = curTonnageByMajorActivity.EnviMajor.TotalWMT;
            ViewBag.EPEP = curTonnageByMajorActivity.EPEP.TotalWMT;
            ViewBag.MAMM = curTonnageByMajorActivity.MiningAreaMngmtMntnce.TotalWMT;
            ViewBag.PitPrep = curTonnageByMajorActivity.PitPrepMiscellaneousAct.TotalWMT;
            ViewBag.Rehandling = curTonnageByMajorActivity.Rehandling.TotalWMT;

            ViewBag.ReportDate = "Report for " + Convert.ToDateTime(chosenDateFrom).ToString("MM/dd/yyyy") + " - " + Convert.ToDateTime(chosenDateTo).ToString("MM/dd/yyyy");
            return View(curTonnageByMajorActivity.BargeShipLoading);
        }

        public ActionResult TonnageByEquipment()
        {
            if (Session["user"] == null)
                Response.Redirect("Login", true);

            List<TonnageByActivity> EquipmentDEURList = (List<TonnageByActivity>)Session["EquipmentDEURList"];
            if (EquipmentDEURList == null)
                EquipmentDEURList = GenerateDEURList(0);

            TonnageByEquipment curTonnageByEquipment = ComputeTonnageByEquipment(EquipmentDEURList);
            if (curTonnageByEquipment == null)
                return RedirectToAction("NoData", "Home");

            ViewBag.EquipmentType = "DUMP TRUCK";
            ViewBag.DumpTruck = curTonnageByEquipment.DumpTruckData.TotalWMT;
            ViewBag.Excavator = curTonnageByEquipment.ExcavatorData.TotalWMT;
            ViewBag.Auxiliary = curTonnageByEquipment.AuxiliaryData.TotalWMT;
            ViewBag.Stationary = curTonnageByEquipment.StationaryData.TotalWMT;
            ViewBag.Envi = curTonnageByEquipment.EnviData.TotalWMT;
            ViewBag.SVST = curTonnageByEquipment.SVSTData.TotalWMT;
            ViewBag.NonEquipment = curTonnageByEquipment.NonEquipmentData.TotalWMT;
            ViewBag.Rainfall = curTonnageByEquipment.RainfallData.TotalWMT;

            string LatestReportDate = (string)Session["ReportDate"];
            ViewBag.ReportDate = "Report for 1/1/" + LatestReportDate.Substring((LatestReportDate.Length - 4), 4) + " - " + LatestReportDate;

            Session["tonnageByEquipmentSession"] = curTonnageByEquipment;
            Session["EquipmentDEURList"] = EquipmentDEURList;
            return View(curTonnageByEquipment.DumpTruckData);
        }
        [HttpPost]
        public ActionResult TonnageByEquipment(DateTime? chosenDateFrom, DateTime? chosenDateTo)
        {
            if (Session["user"] == null)
                Response.Redirect("Login", true);

            List<TonnageByActivity> EquipmentDEURList = new List<TonnageByActivity>();
            EquipmentDEURList = (List<TonnageByActivity>)Session["EquipmentDEURList"];

            if (chosenDateFrom == null || chosenDateTo == null)
                return RedirectToAction("NoData", "Home");
            if (chosenDateFrom > chosenDateTo)
            {
                DateTime? temp = chosenDateFrom;
                chosenDateFrom = chosenDateTo;
                chosenDateTo = temp;
            }

            List<TonnageByActivity> tempDEURList = EquipmentDEURList.Where(s => s.MaterialDateOfEntry >= chosenDateFrom && s.MaterialDateOfEntry <= chosenDateTo).OrderByDescending(s => s.MaterialDateOfEntry).ToList();
            if (tempDEURList.Count == 0)
                return RedirectToAction("NoData", "Home");

            TonnageByEquipment curTonnageByEquipment = ComputeTonnageByEquipment(tempDEURList);
            if (curTonnageByEquipment == null)
                return RedirectToAction("NoData", "Home");

            ViewBag.EquipmentType = "DUMP TRUCK";
            ViewBag.DumpTruck = curTonnageByEquipment.DumpTruckData.TotalWMT;
            ViewBag.Excavator = curTonnageByEquipment.ExcavatorData.TotalWMT;
            ViewBag.Auxiliary = curTonnageByEquipment.AuxiliaryData.TotalWMT;
            ViewBag.Stationary = curTonnageByEquipment.StationaryData.TotalWMT;
            ViewBag.Envi = curTonnageByEquipment.EnviData.TotalWMT;
            ViewBag.SVST = curTonnageByEquipment.SVSTData.TotalWMT;
            ViewBag.NonEquipment = curTonnageByEquipment.NonEquipmentData.TotalWMT;
            ViewBag.Rainfall = curTonnageByEquipment.RainfallData.TotalWMT;
            ViewBag.ReportDate = "Report for " + Convert.ToDateTime(chosenDateFrom).ToString("MM/dd/yyyy") + " - " + Convert.ToDateTime(chosenDateTo).ToString("MM/dd/yyyy");

            Session["tonnageByEquipmentSession"] = curTonnageByEquipment;
            return View(curTonnageByEquipment.DumpTruckData);
        }

        public ActionResult Login()
        {
            Session["user"] = null;
            return View();
        }
        [HttpGet]
        public ActionResult CheckLogin(string username, string password)
        {
            if (_cmcOmtSqlDb.Users.Where(s => s.Username == username && s.Password == password).FirstOrDefault() != null)
            {
                User CurrentUser = _cmcOmtSqlDb.Users.Where(s => s.Username == username && s.Password == password).FirstOrDefault();
                List<Log> UserLog = _cmcOmtSqlDb.Logs.Where(s => s.username == username).OrderByDescending(s => s.startDateTime).Take(5).ToList();
                
                Log curLog = new Log();
                curLog.username = username;
                curLog.activity = "Logged in last " + DateTime.Now;
                curLog.startDateTime = DateTime.Now;

                if (curLog.username != "matt")
                {
                    _cmcOmtSqlDb.Logs.Add(curLog);
                    _cmcOmtSqlDb.SaveChanges();
                }

                Session["user"] = CurrentUser.Username;
                Session["curUser"] = CurrentUser;
                Session["userName"] = CurrentUser.FirstName + " " + CurrentUser.LastName;
                Session["role"] = CurrentUser.Role;
                Session["userLogs"] = UserLog;

                return Json(new { redirecturl = "/Reports/Tonnage" }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { redirecturl = "/Reports/Login" }, JsonRequestBehavior.AllowGet);

        }
    }
}