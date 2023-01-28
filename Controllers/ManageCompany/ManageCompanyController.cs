using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL = FutureTransportBuisenessLayer.ManageCompany;
using BE = FutureTransportEntities.BusinessEntities;

namespace FutureTransport.Controllers.ManageCompany
{
    public class ManageCompanyController : Controller
    {
        // GET: ManageCompany
        BL.ManageCompany manageCompany = new BL.ManageCompany();
        public ActionResult ManageCompany()
        {
            long Userid = Convert.ToInt64(Session["Tracker_userID"]);

            List<BE.ManageCompany> CompanyList = new List<BE.ManageCompany>();
            CompanyList = manageCompany.GetCompanyList(Userid);
            ViewBag.CompanyList = new SelectList(CompanyList, "CompID", "CompanyName");

            return View();
        }

        public JsonResult SaveSubCompanyDetails(List<BE.ManageCompany> CompanyDetails)
        {
            var message = "";
            long Userid = Convert.ToInt64(Session["Tracker_userID"]);

            message = manageCompany.SaveSubCompanyDetails(CompanyDetails, Userid);

            return Json(message);
        }

        public JsonResult SaveFactoryDetails(List<BE.ManageFactory> FactoryDetails)
        {
            var message = "";
            long Userid = Convert.ToInt64(Session["Tracker_userID"]);

            message = manageCompany.SaveFactoryDetails(FactoryDetails, Userid);

            return Json(message);
        }

        public JsonResult GetCompanySummaryDetails(string Userid)
        {
            List<BE.ManageCompany> userList = new List<BE.ManageCompany>();
            userList = manageCompany.GetCompanySummaryDetails(Userid);
            var JsonResult = Json(userList, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }
        public JsonResult GetFactorySummaryDetails(string Userid)
        {
            List<BE.ManageCompany> userList = new List<BE.ManageCompany>();
            userList = manageCompany.GetFactorySummaryDetails(Userid);
            var JsonResult = Json(userList, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }
    }
}