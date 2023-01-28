using FutureTransport.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = FutureTransportEntities.BusinessEntities;
using BL = FutureTransportBuisenessLayer.Vehicle;

namespace FutureTransport.Controllers.Vehicle
{
    [UserAuthenticationFilter]
    public class VehicleController : Controller
    {
        BL.VehicleDataProvider DataProvider = new BL.VehicleDataProvider();

        public ActionResult VehicleRegistration()
        {
            List<BE.Vehicles> vehicles = DataProvider.GetVehicleModel();
            ViewBag.VehicleModel = new SelectList(vehicles, "VehicleModel", "VehicleModel");
            return View();
        }

        public JsonResult AjaxVehicleRegistration(BE.Vehicles VD)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            message = DataProvider.VehicleRegistration(VD);
            return Json(message);
        }

        public JsonResult GetVehicles()
        {
            List<BE.Vehicles> list = new List<BE.Vehicles>();
            list = DataProvider.GetVehicles();
            return Json(list);
        }
    }
}