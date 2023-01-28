using FutureTransport.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = FutureTransportEntities.BusinessEntities;
using BL = FutureTransportBuisenessLayer.VendorBiddings;

namespace FutureTransport.Controllers.VendorBiddings
{
    [UserAuthenticationFilter]
    public class VendorDetailsController : Controller
    {
        BL.VendorBiddingsDataProvider DataProvider = new BL.VendorBiddingsDataProvider();

        public ActionResult VendorBiddings()
        {
            return View();
        }

        public JsonResult GetVendorBiddings(BE.Bookings VD)
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetVendorBiddings(VD);
            return Json(list);
        }

        public JsonResult AjaxAcceptedBooking(string BN, int VI)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            message = DataProvider.AcceptedBooking(BN, VI);
            return Json(message);
        }

        public JsonResult AjaxRejectedBooking(string RD)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            message = DataProvider.RejectedBooking(RD);
            return Json(message);
        }
    }
}