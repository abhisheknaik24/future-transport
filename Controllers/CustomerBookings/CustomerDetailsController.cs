using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL = FutureTransportBuisenessLayer.CustomerBookings;
using BE = FutureTransportEntities.BusinessEntities;
using FutureTransport.Filters;

namespace FutureTransport.Controllers.CustomerBookings
{
    [UserAuthenticationFilter]
    public class CustomerDetailsController : Controller
    {
        BL.CustomerBookingsDataProvider DataProvider = new BL.CustomerBookingsDataProvider();

        public ActionResult CustomerBookings()
        {
            return View();
        }

        public JsonResult GetCustomerBookings(BE.Bookings CD)
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetCustomerBookings(CD);
            return Json(list);
        }

        public ActionResult BookingDetails(string BookingNo)
        {
            ViewBag.BookingNo = BookingNo;
            return PartialView();
        }

        public JsonResult GetBookingDetails(string BN)
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetBookingDetails(BN);
            return Json(list);
        }
    }
}