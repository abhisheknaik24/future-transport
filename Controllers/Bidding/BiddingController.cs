using FutureTransport.Filters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BE = FutureTransportEntities.BusinessEntities;
using BL = FutureTransportBuisenessLayer.Bidding;

namespace FutureTransport.Controllers.Bidding
{
    [UserAuthenticationFilter]
    public class BiddingController : Controller
    {
        BL.BiddingDataProvider DataProvider = new BL.BiddingDataProvider();

        public ActionResult Bidding()
        {
            int BookingId = 0;
            string BookingNo = "0";
            int VendorId = Convert.ToInt16(Session["UserId"]);

            BE.Bookings data = new BE.Bookings();
            if (BookingId != 0 && BookingNo != "0")
            {
                ViewBag.BookingId = BookingId;
                ViewBag.BookingNo = BookingNo;
            }
            else
            {
                ViewBag.BookingId = 0;
                ViewBag.BookingNo = "0";
            }
            ViewBag.VendorId = VendorId;
            return View();
        }

        public JsonResult GetBiddings(BE.Bookings BD)
        {
            int VendorId = Convert.ToInt16(Session["UserId"]);
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetBiddings(BD, VendorId);
            return Json(list);
        }

        public JsonResult AjaxAcceptedBidding(BE.Bookings SD)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            message = DataProvider.AcceptedBidding(SD);
            return Json(message);
        }

        public ActionResult LiveBids()
        {
            int BookingId = 0;
            string BookingNo = "0";

            BE.Bookings data = new BE.Bookings();
            if (BookingId != 0 && BookingNo != "0")
            {
                ViewBag.BookingId = BookingId;
                ViewBag.BookingNo = BookingNo;
            }
            else
            {
                ViewBag.BookingId = 0;
                ViewBag.BookingNo = "0";
            }
            return View();
        }

        public JsonResult GetLiveBiddings(BE.Bookings GD)
        {
            int VendorId = Convert.ToInt16(Session["UserId"]);
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetLiveBiddings(GD, VendorId);
            return Json(list);
        }

        public ActionResult PendingMovements()
        {
            return View();
        }

        public JsonResult GetPendingMovements(BE.Bookings GD)
        {
            int VendorId = Convert.ToInt16(Session["UserId"]);
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetPendingMovements(GD, VendorId);
            return Json(list);
        }

        public ActionResult BookingDetails(string BookingNo)
        {
            List<BE.Drivers> drivers = DataProvider.GetDrivers();
            ViewBag.DriverDropdown = new SelectList(drivers, "DriverId", "DriverName");
            List<BE.Vehicles> vehicles = DataProvider.GetVehicles();
            ViewBag.VehicleDropdown = new SelectList(vehicles, "VehicleId", "VehicleNumber");
            ViewBag.BookingNo = BookingNo;
            return PartialView();
        }

        public JsonResult GetBookingDetails(string BN)
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetBookingDetails(BN);
            return Json(list);
        }

        public JsonResult AjaxAddDriver(BE.Bookings SD)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            message = DataProvider.UpdateAddDriver(SD);
            return Json(message);
        }

        public JsonResult AjaxAddVehicle(BE.Bookings SV)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            message = DataProvider.UpdateAddVehicle(SV);
            return Json(message);
        }

        public ActionResult BookingHistory()
        {
            return View();
        }

        public JsonResult GetBookingsHistory(BE.Bookings GDH)
        {
            int VendorId = Convert.ToInt16(Session["UserId"]);
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetBookingsHistory(GDH, VendorId);
            return Json(list);
        }

        public ActionResult BookingDocuments()
        {
            return View();
        }

        public JsonResult GetBookingDocuments(BE.Bookings GDL)
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetBookingDocuments(GDL);
            return Json(list);
        }

        public ActionResult LiveTrips()
        {
            return View();
        }

        public JsonResult GetBookingsLiveTrips()
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetBookingsLiveTrips();
            return Json(list);
        }

        public ActionResult LiveTripsDetails(string BookingNo)
        {
            ViewBag.BookingNo = BookingNo;
            return PartialView();
        }

        public JsonResult GetLiveTripsDetails(string BN)
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetLiveTripsDetails(BN);
            return Json(list);
        }

        public JsonResult GetUpdateDate(string BN, string SN)
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetUpdateDate(BN, SN);
            return Json(list);
        }

        public JsonResult AjaxUpdateDropOffBooking(BE.Bookings UDD)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            message = DataProvider.UpdateDropOffBooking(UDD);
            return Json(message);
        }

        public ActionResult TripsCompleted()
        {
            return View();
        }

        public JsonResult GetBookingsTripsCompleted(BE.Bookings GDC)
        {
            int VendorId = Convert.ToInt16(Session["UserId"]);
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetBookingsTripsCompleted(GDC, VendorId);
            return Json(list);
        }

        public ActionResult TripsCompletedDetails(string BookingNo)
        {
            ViewBag.BookingNo = BookingNo;
            return PartialView();
        }

        public JsonResult GetTripsCompletedDetails(string BN)
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetTripsCompletedDetails(BN);
            return Json(list);
        }

        public ActionResult Documents(string BookingNo)
        {
            ViewBag.BookingNo = BookingNo;
            return PartialView();
        }

        [HttpPost]
        public ActionResult ShowDeliveryAttachment(string BookingNo)
        {
            ViewBag.BookingNo = BookingNo;
            return PartialView();
        }

        [HttpPost]
        public ActionResult StoreDeliveryAttachment(string DocName, string FilePath, string ContentType)
        {
            TempData["DocName"] = DocName;
            TempData["FilePath"] = FilePath;
            TempData["ContentType"] = ContentType;
            int i = 1;
            return Json(i);
        }

        public FileResult DownloadDeliveryAttachment(int id)
        {
            string DocName = Convert.ToString(TempData["DocName"]);
            string FilePath = Convert.ToString(TempData["FilePath"]);
            string ContentType = Convert.ToString(TempData["ContentType"]);
            byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
            return File(fileBytes, ContentType, DocName);
        }

        public JsonResult GetDeliveryAttachment(string DD)
        {
            List<BE.Documents> list = new List<BE.Documents>();
            list = DataProvider.GetDeliveryAttachment(DD);
            return Json(list);
        }

        public ActionResult LorryAttachment()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AjaxUploadLorryAttachment(BE.Documents fileData)
        {
            BE.Documents documents = new BE.Documents();
            if (Request.Files.Count > 0)
            {
                try
                {
                    int Userid = Convert.ToInt16(Session["UserId"]);
                    string Type = fileData.EnqType;
                    Type = Type + Userid;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string root = Server.MapPath("~/Uploads/Lorry/" + Type + "/");
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        string contentType;
                        contentType = MimeMapping.GetMimeMapping(fname);

                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }
                        string check = Path.Combine(Server.MapPath("~/Uploads/Lorry/" + Type + "/"), fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(check);
                        documents.DocName = fname;
                        documents.FilePath = check;
                        documents.ContentType = contentType;
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            return Json(documents);
        }

        [HttpPost]
        public ActionResult StoreLorryAttachment(string DocName, string FilePath, string ContentType)
        {
            TempData["DocName"] = DocName;
            TempData["FilePath"] = FilePath;
            TempData["ContentType"] = ContentType;
            int i = 1;
            return Json(i);
        }

        public FileResult DownloadLorryAttachment(int id)
        {
            string DocName = Convert.ToString(TempData["DocName"]);
            string FilePath = Convert.ToString(TempData["FilePath"]);
            string ContentType = Convert.ToString(TempData["ContentType"]);
            byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
            return File(fileBytes, ContentType, DocName);
        }

        public JsonResult AjaxLorryAttachment(string BN, List<BE.Documents> A)
        {
            int AddedBy = Convert.ToInt16(Session["UserId"]);
            BE.ResponseMessage message = new BE.ResponseMessage();

            if (A != null)
            {
                for (int j = 0; j < A.Count; j++)
                {
                    string root = Server.MapPath("~/Uploads/LorryAttachment/LorryAttachmentFor" + BN + "/");
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    string oldPath = A[j].FilePath;
                    string newPath = Path.Combine(Server.MapPath("~/Uploads/LorryAttachment/LorryAttachmentFor" + BN + "/"), A[j].DocName);
                    if (!System.IO.File.Exists(newPath))
                    {
                        System.IO.File.Move(oldPath, newPath);

                    }
                    A[j].FilePath = newPath;
                }
            }
            if (A != null)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("DocId");
                dataTable.Columns.Add("BookingNo");
                dataTable.Columns.Add("UploadFor");
                dataTable.Columns.Add("DocName");
                dataTable.Columns.Add("FilePath");
                dataTable.Columns.Add("ContentType");
                dataTable.TableName = "PT_LorryAttachment";
                foreach (BE.Documents item in A)
                {
                    DataRow row = dataTable.NewRow();
                    row["DocId"] = item.DocId;
                    row["BookingNo"] = BN;
                    row["UploadFor"] = item.UploadFor;
                    row["DocName"] = item.DocName;
                    row["FilePath"] = item.FilePath;
                    row["ContentType"] = item.ContentType;
                    dataTable.Rows.Add(row);
                }
                message = DataProvider.SaveLorryAttachment(dataTable, AddedBy);
            }
            return Json(message);
        }

        public ActionResult OffloadingAttachment()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AjaxUploadOffloadingAttachment(BE.Documents fileData)
        {
            BE.Documents documents = new BE.Documents();
            if (Request.Files.Count > 0)
            {
                try
                {
                    int Userid = Convert.ToInt16(Session["UserId"]);
                    string Type = fileData.EnqType;
                    Type = Type + Userid;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string root = Server.MapPath("~/Uploads/Offloading/" + Type + "/");
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        string contentType;
                        contentType = MimeMapping.GetMimeMapping(fname);

                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }
                        string check = Path.Combine(Server.MapPath("~/Uploads/Offloading/" + Type + "/"), fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(check);
                        documents.DocName = fname;
                        documents.FilePath = check;
                        documents.ContentType = contentType;
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            return Json(documents);
        }

        [HttpPost]
        public ActionResult StoreOffloadingAttachment(string DocName, string FilePath, string ContentType)
        {
            TempData["DocName"] = DocName;
            TempData["FilePath"] = FilePath;
            TempData["ContentType"] = ContentType;
            int i = 1;
            return Json(i);
        }

        public FileResult DownloadOffloadingAttachment(int id)
        {
            string DocName = Convert.ToString(TempData["DocName"]);
            string FilePath = Convert.ToString(TempData["FilePath"]);
            string ContentType = Convert.ToString(TempData["ContentType"]);
            byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
            return File(fileBytes, ContentType, DocName);
        }

        public JsonResult AjaxOffloadingAttachment(string BN, List<BE.Documents> A)
        {
            int AddedBy = Convert.ToInt16(Session["UserId"]);
            BE.ResponseMessage message = new BE.ResponseMessage();

            if (A != null)
            {
                for (int j = 0; j < A.Count; j++)
                {
                    string root = Server.MapPath("~/Uploads/OffloadingAttachment/OffloadingAttachmentFor" + BN + "/");
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    string oldPath = A[j].FilePath;
                    string newPath = Path.Combine(Server.MapPath("~/Uploads/OffloadingAttachment/OffloadingAttachmentFor" + BN + "/"), A[j].DocName);
                    if (!System.IO.File.Exists(newPath))
                    {
                        System.IO.File.Move(oldPath, newPath);

                    }
                    A[j].FilePath = newPath;
                }
            }
            if (A != null)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("DocId");
                dataTable.Columns.Add("BookingNo");
                dataTable.Columns.Add("UploadFor");
                dataTable.Columns.Add("DocName");
                dataTable.Columns.Add("FilePath");
                dataTable.Columns.Add("ContentType");
                dataTable.TableName = "PT_OffloadingAttachment";
                foreach (BE.Documents item in A)
                {
                    DataRow row = dataTable.NewRow();
                    row["DocId"] = item.DocId;
                    row["BookingNo"] = BN;
                    row["UploadFor"] = item.UploadFor;
                    row["DocName"] = item.DocName;
                    row["FilePath"] = item.FilePath;
                    row["ContentType"] = item.ContentType;
                    dataTable.Rows.Add(row);
                }
                message = DataProvider.SaveOffloadingAttachment(dataTable, AddedBy);
            }
            return Json(message);
        }

        public ActionResult AdditionalAttachment()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AjaxUploadAdditionalAttachment(BE.Documents fileData)
        {
            BE.Documents documents = new BE.Documents();
            if (Request.Files.Count > 0)
            {
                try
                {
                    int Userid = Convert.ToInt16(Session["UserId"]);
                    string Type = fileData.EnqType;
                    Type = Type + Userid;
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;
                        string root = Server.MapPath("~/Uploads/AdditionalDocument/" + Type + "/");
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        string contentType;
                        contentType = MimeMapping.GetMimeMapping(fname);

                        if (!Directory.Exists(root))
                        {
                            Directory.CreateDirectory(root);
                        }
                        string check = Path.Combine(Server.MapPath("~/Uploads/AdditionalDocument/" + Type + "/"), fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(check);
                        documents.DocName = fname;
                        documents.FilePath = check;
                        documents.ContentType = contentType;
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            return Json(documents);
        }

        [HttpPost]
        public ActionResult StoreAdditionalAttachment(string DocName, string FilePath, string ContentType)
        {
            TempData["DocName"] = DocName;
            TempData["FilePath"] = FilePath;
            TempData["ContentType"] = ContentType;
            int i = 1;
            return Json(i);
        }

        public FileResult DownloadAdditionalAttachment(int id)
        {
            string DocName = Convert.ToString(TempData["DocName"]);
            string FilePath = Convert.ToString(TempData["FilePath"]);
            string ContentType = Convert.ToString(TempData["ContentType"]);
            byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);
            return File(fileBytes, ContentType, DocName);
        }

        public JsonResult AjaxAdditionalAttachment(string BN, List<BE.Documents> A)
        {
            int AddedBy = Convert.ToInt16(Session["UserId"]);
            BE.ResponseMessage message = new BE.ResponseMessage();

            if (A != null)
            {
                for (int j = 0; j < A.Count; j++)
                {
                    string root = Server.MapPath("~/Uploads/AdditionalDocumentAttachment/AdditionalDocumentFor" + BN + "/");
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    string oldPath = A[j].FilePath;
                    string newPath = Path.Combine(Server.MapPath("~/Uploads/AdditionalDocumentAttachment/AdditionalDocumentFor" + BN + "/"), A[j].DocName);
                    if (!System.IO.File.Exists(newPath))
                    {
                        System.IO.File.Move(oldPath, newPath);

                    }

                    A[j].FilePath = newPath;
                }
            }
            if (A != null)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("DocId");
                dataTable.Columns.Add("BookingNo");
                dataTable.Columns.Add("UploadFor");
                dataTable.Columns.Add("DocName");
                dataTable.Columns.Add("FilePath");
                dataTable.Columns.Add("ContentType");
                dataTable.TableName = "PT_AdditionalAttachment";
                foreach (BE.Documents item in A)
                {
                    DataRow row = dataTable.NewRow();
                    row["DocId"] = item.DocId;
                    row["BookingNo"] = BN;
                    row["UploadFor"] = item.UploadFor;
                    row["DocName"] = item.DocName;
                    row["FilePath"] = item.FilePath;
                    row["ContentType"] = item.ContentType;
                    dataTable.Rows.Add(row);
                }
                message = DataProvider.SaveAdditionalAttachment(dataTable, AddedBy);
            }
            return Json(message);
        }
    }
}