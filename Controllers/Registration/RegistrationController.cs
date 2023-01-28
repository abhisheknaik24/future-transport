using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Data;
using BL = FutureTransportBuisenessLayer.Registration;
using BE = FutureTransportEntities.BusinessEntities;

namespace FutureTransport.Controllers.Registration
{
    public class RegistrationController : Controller
    {
        BL.RegistrationDataProvider DataProvider = new BL.RegistrationDataProvider();

        public ActionResult Registration()
        {
            return View();
        }

        public JsonResult SendEmail(string EmailId, string OTP, string CustomerMsg)
        {
            string message = "";
            Boolean isMessageSent = false;
            message = DataProvider.SendEmail(EmailId, OTP, CustomerMsg);
            if (isMessageSent == true)
            {
                message = "Mail Sent";
            }
            else
            {
                message = "Mail Not Sent";
            }
            return Json(message);
        }

        [HttpPost]
        public JsonResult InsertOTPEmail(string EmailId, string OTP)
        {
            var message = "";
            message = DataProvider.InsertOTPEmail(EmailId, OTP);
            return Json(message);
        }

        [HttpPost]
        public JsonResult VerifyOTPEmail(string OTP)
        {
            List<BE.OTPModel> locations = DataProvider.VerifyOTPEmail(OTP);
            var jsonResult = Json(locations, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }

        [HttpPost]
        public JsonResult SaveRegistrationDetails(List<BE.Registration> RD, string EmailId, string ContactNo, string Username, string Department)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            DataTable dtCustomerDetails = new DataTable();
            dtCustomerDetails.Columns.Add("EmailId");
            dtCustomerDetails.Columns.Add("ContactNo");
            dtCustomerDetails.Columns.Add("Department");
            dtCustomerDetails.Columns.Add("PersonName");
            dtCustomerDetails.Columns.Add("Designation");
            dtCustomerDetails.Columns.Add("MobileNo");
            dtCustomerDetails.Columns.Add("Email");
            dtCustomerDetails.Columns.Add("Address");
            dtCustomerDetails.Columns.Add("State");
            dtCustomerDetails.Columns.Add("City");
            dtCustomerDetails.Columns.Add("Pincode");
            dtCustomerDetails.TableName = "PT_InsertRegistrationDetails";

            foreach (BE.Registration element in RD)
            {
                DataRow row = dtCustomerDetails.NewRow();
                row["EmailId"] = element.EmailId.Trim();
                row["ContactNo"] = element.ContactNo.Trim();
                row["Department"] = element.Department.Trim();
                row["PersonName"] = element.PersonName.Trim();
                row["Designation"] = element.Designation.Trim();
                row["MobileNo"] = element.MobileNo.Trim();
                row["Email"] = element.Email.Trim();
                row["Address"] = element.Address.Trim().Replace("\n", "").Replace("\r", "");
                row["State"] = element.State.Trim();
                row["City"] = element.City.Trim();
                row["PinCode"] = element.PinCode.Trim();
                dtCustomerDetails.Rows.Add(row);
            }
            message = DataProvider.SaveRegistrationDetails(dtCustomerDetails, EmailId, ContactNo, Username, Department);
            return Json(message);
        }
    }
}