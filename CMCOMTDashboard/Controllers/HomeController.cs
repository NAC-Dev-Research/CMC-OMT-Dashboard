using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using CMCOMTDashboard.DAL;
using CMCOMTDashboard.Models;
using PagedList;

namespace CMCOMTDashboard.Controllers
{
    public class HomeController : Controller
    {
        CMCOMTDashboardEntities _cmcOmtSqlDb;
        public HomeController()
        {
            _cmcOmtSqlDb = new CMCOMTDashboardEntities();
        }

        public ActionResult MainPage()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Reports");

            return View();
        }

        public ActionResult ComingSoon()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Reports");

            return View();
        }

        public ActionResult NoData()
        {
            return View();
        }

        public ActionResult CreateAccount()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Reports");

            return View();
        }
        [HttpPost]
        public ActionResult CreateAccount(string username, string password, string firstName, string lastName, string siteLocation, string role, string btn_addAccount)
        {
            if (btn_addAccount.ToUpper() == "YES")
            {
                User curUser = new User();
                curUser.Username = username;
                curUser.Password = password;
                curUser.FirstName = firstName;
                curUser.LastName = lastName;
                curUser.Site = siteLocation;
                curUser.Role = role;

                _cmcOmtSqlDb.Users.Add(curUser);
                _cmcOmtSqlDb.SaveChanges();
            }

            return View();
        }

        public ActionResult ChangePassword()
        {
            if (Session["user"] == null)
                return RedirectToAction("Login", "Reports");

            ViewBag.Notif = "";
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(string curPassword, string newPassword, string reEnterPassword)
        {
            User curUser = (User)Session["curUser"]; //dangerous, think of another way
            if (curUser.Password == curPassword)
            {
                if (newPassword == reEnterPassword)
                {
                    User updatedUser = _cmcOmtSqlDb.Users.Where(s => s.Username == curUser.Username && s.Password == curUser.Password).FirstOrDefault();
                    updatedUser.Password = newPassword;
                    _cmcOmtSqlDb.Entry(updatedUser).State = System.Data.Entity.EntityState.Modified;
                    _cmcOmtSqlDb.SaveChanges();

                    Thread.Sleep(2000);
                    Session["user"] = null;
                    Session.Clear();
                    return RedirectToAction("Login", "Reports");
                }
                else
                    ViewBag.Notif = "New and re-entered password don't match. Please try again.";
            }
            else
                ViewBag.Notif = "Current password is incorrect. Please try again.";

            return View();
        }

        public ActionResult Logs(int? page)
        {
            if (Session["user"] == null)
                Response.Redirect("Login", true);

            List<Log> curLogs = _cmcOmtSqlDb.Logs.OrderByDescending(s => s.startDateTime).ToList();

            int pageSize = 15;
            int pageNumber = (page ?? 1);

            return View(curLogs.ToPagedList(pageNumber, pageSize));
        }
    }
}