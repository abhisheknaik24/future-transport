using FutureTransport.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = FutureTransportEntities.BusinessEntities;
using BL = FutureTransportBuisenessLayer.CustomerDashboard;

namespace FutureTransport.Controllers.CustomerDashboard
{
    [UserAuthenticationFilter]
    public class CustomerDashboardController : Controller
    {
        BL.CustomerDashboardDataProvider DataProvider = new BL.CustomerDashboardDataProvider();

        public ActionResult CustomerDashboard()
        {
            List<BE.CustomerDashboard> list = new List<BE.CustomerDashboard>();
            list = DataProvider.GetBiddingCount();
            ViewBag.List = list;
            return View();
        }
    }
}