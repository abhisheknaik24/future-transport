using FutureTransport.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = FutureTransportEntities.BusinessEntities;
using BL = FutureTransportBuisenessLayer.VendorDashboard;

namespace FutureTransport.Controllers.VendorDashboard
{
    [UserAuthenticationFilter]
    public class VendorDashboardController : Controller
    {
        BL.VendorDashboardDataProvider DataProvider = new BL.VendorDashboardDataProvider();

        public ActionResult VendorDashboard()
        {
            List<BE.VendorDashboard> list = new List<BE.VendorDashboard>();
            list = DataProvider.GetBookingCount();
            ViewBag.List = list;
            return View();
        }
    }
}