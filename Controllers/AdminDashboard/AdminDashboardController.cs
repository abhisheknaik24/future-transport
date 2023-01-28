using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL = FutureTransportBuisenessLayer.AdminDashboard;
using BE = FutureTransportEntities.BusinessEntities;
using FutureTransport.Filters;

namespace FutureTransport.Controllers.AdminDashboard
{
    [UserAuthenticationFilter]
    public class AdminDashboardController : Controller
    {
        BL.AdminDashboardDataProvider DataProvider = new BL.AdminDashboardDataProvider();
        public ActionResult AdminDashboard()
        {
            return View();
        }

        public JsonResult GetAdminDashboard()
        {
            List<BE.AdminDashboard> list = new List<BE.AdminDashboard>();
            list = DataProvider.GetAdminDashboard();
            return Json(list);
        }
    }
}