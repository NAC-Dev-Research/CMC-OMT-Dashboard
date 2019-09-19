using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CMCOMTDashboard.DAL;
using CMCOMTDashboard.Models;

namespace CMCOMTDashboard.Controllers
{
    public class ReportsHttpGetController : ComputeTonnageController
    {
        CMCOMTDbContext _cmcOmtDb;
        CMCOMTDashboardEntities _cmcOmtSqlDb;
        public ReportsHttpGetController()
        {
            _cmcOmtDb = new CMCOMTDbContext();
            _cmcOmtSqlDb = new CMCOMTDashboardEntities();
        }

        [HttpGet]
        public JsonResult GetTonnageByActivity(string majorActivity)
        {
            MajorActivityDetails majorActivityDetails = new MajorActivityDetails();
            TonnageByMajorActivity curTonnageByMajorActivity = (TonnageByMajorActivity)Session["tonnageByActivitySession"];

            if (majorActivity.Trim() == "Bargeloading/Shiploading")
                majorActivityDetails = curTonnageByMajorActivity.BargeShipLoading;
            else if (majorActivity.Trim() == "Crushing")
                majorActivityDetails = curTonnageByMajorActivity.Crushing;
            else if (majorActivity.Trim() == "Direct Mining")
                majorActivityDetails = curTonnageByMajorActivity.DirectMining;
            else if (majorActivity.Trim() == "Employee Servicing")
                majorActivityDetails = curTonnageByMajorActivity.EmployeeServicing;
            else if (majorActivity.Trim() == "Envi Major")
                majorActivityDetails = curTonnageByMajorActivity.EnviMajor;
            else if (majorActivity.Trim() == "EPEP")
                majorActivityDetails = curTonnageByMajorActivity.EPEP;
            else if (majorActivity.Trim() == "Mining")
                majorActivityDetails = curTonnageByMajorActivity.Mining;
            else if (majorActivity.Trim() == "Mining Area Management and Maintenance")
                majorActivityDetails = curTonnageByMajorActivity.MiningAreaMngmtMntnce;
            else if (majorActivity.Trim() == "Pit Prep and Miscellaneous Activities")
                majorActivityDetails = curTonnageByMajorActivity.PitPrepMiscellaneousAct;
            else if (majorActivity.Trim() == "Project")
                majorActivityDetails = curTonnageByMajorActivity.Project;
            else if (majorActivity.Trim() == "Rehandling")
                majorActivityDetails = curTonnageByMajorActivity.Rehandling;

            System.Diagnostics.Debug.WriteLine("Total WMT: " + majorActivityDetails.TotalWMT);
            ViewBag.MajorActivity = majorActivity.ToUpper();
            return Json(majorActivityDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTonnageByEquipment(string equipmentType)
        {
            TonnageByEquipment curTonnageByEquipment = (TonnageByEquipment)Session["tonnageByEquipmentSession"];
            MajorActivityDetails majorActivityDetails = new MajorActivityDetails();

            if (equipmentType.Trim() == "Dump Truck")
                majorActivityDetails = curTonnageByEquipment.DumpTruckData;
            else if (equipmentType.Trim() == "Excavator")
                majorActivityDetails = curTonnageByEquipment.ExcavatorData;
            else if (equipmentType.Trim() == "Auxiliary")
                majorActivityDetails = curTonnageByEquipment.AuxiliaryData;
            else if (equipmentType.Trim() == "Stationary")
                majorActivityDetails = curTonnageByEquipment.StationaryData;
            else if (equipmentType.Trim() == "Envi")
                majorActivityDetails = curTonnageByEquipment.EnviData;
            else if (equipmentType.Trim() == "SVST")
                majorActivityDetails = curTonnageByEquipment.SVSTData;
            else if (equipmentType.Trim() == "Non Equipment")
                majorActivityDetails = curTonnageByEquipment.NonEquipmentData;
            else if (equipmentType.Trim() == "Rainfall")
                majorActivityDetails = curTonnageByEquipment.RainfallData;

            ViewBag.MajorActivity = equipmentType.ToUpper();
            return Json(majorActivityDetails, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetUserLogs()
        {
            _cmcOmtSqlDb.Configuration.ProxyCreationEnabled = false;

            User curUserName = (User)Session["curUser"];
            //List<Log> logList = _cmcOmtSqlDb.Logs.Where(s => s.username == curUserName.Username).OrderByDescending(s => s.startDateTime).Take(5).ToList();
            List<Log> logList = new List<Log>();

            return Json(logList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult LogoutUser()
        {
            Session["user"] = null;
            Session.Clear();
            return Json(new { redirecturl = "/Reports/Login" }, JsonRequestBehavior.AllowGet);
        }
    }
}