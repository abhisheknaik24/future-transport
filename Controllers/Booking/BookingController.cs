using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL = FutureTransportBuisenessLayer.Booking;
using BE = FutureTransportEntities.BusinessEntities;
using FutureTransport.Filters;
using HL = FutureTransportDataLayer.Helper;
using QRCoder;
using System.Drawing;

namespace FutureTransport.Controllers.Booking
{
    [UserAuthenticationFilter]
    public class BookingController : Controller
    {
        BL.BookingDataProvider DataProvider = new BL.BookingDataProvider();
        HL.DBOperationsForCustomerPortal DBHelper = new HL.DBOperationsForCustomerPortal();

        public ActionResult LiveBookings()
        {
            int BookingId = 0;
            string BookingNo = "0";
            int UserId = Convert.ToInt16(Session["UserId"]);

            List<BE.ContainerTypes> containerTypes = DataProvider.GetContainerTypes();
            ViewBag.CTDropdown = new SelectList(containerTypes, "ContainerTypeID", "ContainerType");
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
            ViewBag.UserId = UserId;
            return View();
        }

        public JsonResult GetBookings(string ShipmentType)
        {
            int UserId = Convert.ToInt16(Session["UserId"]);
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetBookings(ShipmentType, UserId);
            return Json(list);
        }

        public JsonResult AjaxAddBooking(List<BE.Bookings> BD)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            if (BD != null)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("BookingId");
                dataTable.Columns.Add("BookingNo");
                dataTable.Columns.Add("UserId");
                dataTable.Columns.Add("ShipmentType");
                dataTable.Columns.Add("PlanDate");
                dataTable.Columns.Add("PickUpLocation");
                dataTable.Columns.Add("DropOffLocation");
                dataTable.Columns.Add("ContainerSizeTwenty");
                dataTable.Columns.Add("ContainerTypeTwenty");
                dataTable.Columns.Add("WeightTwenty");
                dataTable.Columns.Add("WeightTwentyUnit");
                dataTable.Columns.Add("QuantityTwenty");
                dataTable.Columns.Add("ContainerSizeFourty");
                dataTable.Columns.Add("ContainerTypeFourty");
                dataTable.Columns.Add("WeightFourty");
                dataTable.Columns.Add("WeightFourtyUnit");
                dataTable.Columns.Add("QuantityFourty");
                dataTable.Columns.Add("ContainerSizeFourtyFive");
                dataTable.Columns.Add("ContainerTypeFourtyFive");
                dataTable.Columns.Add("WeightFourtyFive");
                dataTable.Columns.Add("WeightFourtyFiveUnit");
                dataTable.Columns.Add("QuantityFourtyFive");
                dataTable.TableName = "PT_AddBooking";
                foreach (BE.Bookings item in BD)
                {
                    DataRow row = dataTable.NewRow();
                    row["BookingId"] = item.BookingId;
                    row["BookingNo"] = item.BookingNo;
                    row["UserId"] = item.UserId;
                    row["ShipmentType"] = item.ShipmentType;
                    row["PlanDate"] = item.PlanDate;
                    row["PickUpLocation"] = item.PickUpLocation;
                    row["DropOffLocation"] = item.DropOffLocation;
                    row["ContainerSizeTwenty"] = item.ContainerSizeTwenty;
                    row["ContainerTypeTwenty"] = item.ContainerTypeTwenty;
                    row["WeightTwenty"] = item.WeightTwenty;
                    row["WeightTwentyUnit"] = item.WeightTwentyUnit;
                    row["QuantityTwenty"] = item.QuantityTwenty;
                    row["ContainerSizeFourty"] = item.ContainerSizeFourty;
                    row["ContainerTypeFourty"] = item.ContainerTypeFourty;
                    row["WeightFourty"] = item.WeightFourty;
                    row["WeightFourtyUnit"] = item.WeightFourtyUnit;
                    row["QuantityFourty"] = item.QuantityFourty;
                    row["ContainerSizeFourtyFive"] = item.ContainerSizeFourtyFive;
                    row["ContainerTypeFourtyFive"] = item.ContainerTypeFourtyFive;
                    row["WeightFourtyFive"] = item.WeightFourtyFive;
                    row["WeightFourtyFiveUnit"] = item.WeightFourtyFiveUnit;
                    row["QuantityFourtyFive"] = item.QuantityFourtyFive;
                    dataTable.Rows.Add(row);
                }
                message = DataProvider.AddBooking(dataTable);
            }
            return Json(message);
        }

        public ActionResult BookingDetails(string BookingNo)
        {
            ViewBag.BookingNo = BookingNo;
            return PartialView();
        }

        public JsonResult AjaxDeleteBooking(string DD)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            message = DataProvider.DeleteBooking(DD);
            return Json(message);
        }

        public JsonResult AjaxAcceptedBooking(string AD, string EI)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            string email = "";
            message = DataProvider.AcceptedBooking(AD);
            email = DataProvider.SendEmailAcceptedBooking(EI);
            return Json(message);
        }

        public JsonResult AjaxRejectedBooking(string RD)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            message = DataProvider.RejectedBooking(RD);
            return Json(message);
        }

        public JsonResult GetBookingDetails(string BN)
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetBookingDetails(BN);
            return Json(list);
        }

        public JsonResult AjaxAddBookingDetails(List<BE.Bookings> CN)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            if (CN != null)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("BookingNo");
                dataTable.Columns.Add("SizeNo");
                dataTable.Columns.Add("ContainerNo");
                dataTable.Columns.Add("PickUpDate");
                dataTable.TableName = "PT_AddBookingDetails";
                foreach (BE.Bookings item in CN)
                {
                    DataRow row = dataTable.NewRow();
                    row["BookingNo"] = item.BookingNo;
                    row["SizeNo"] = item.SizeNo;
                    row["ContainerNo"] = item.ContainerNo;
                    row["PickUpDate"] = item.PickUpDate;
                    dataTable.Rows.Add(row);
                }
                message = DataProvider.AddBookingDetails(dataTable);
            }
            return Json(message);
        }

        public JsonResult AjaxUpdatePickUpBooking(BE.Bookings UPD)
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            message = DataProvider.UpdatePickUpBooking(UPD);
            return Json(message);
        }

        public ActionResult PendingMovements()
        {
            return View();
        }

        public JsonResult GetPendingMovements(BE.Bookings GD)
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetPendingMovements(GD);
            return Json(list);
        }

        public ActionResult BookingHistory()
        {
            return View();
        }

        public JsonResult GetBookingsHistory(string ShipmentType)
        {
            int UserId = Convert.ToInt16(Session["UserId"]);
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetBookingsHistory(ShipmentType, UserId);
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

        [HttpPost]
        public ActionResult ShowDocuments(string BookingNo)
        {
            ViewBag.BookingNo = BookingNo;
            return PartialView();
        }

        [HttpPost]
        public ActionResult StoreDocuments(string DocNameA, string FilepathA, string ContentTypeA)
        {
            TempData["DocName"] = DocNameA;
            TempData["FilePath"] = FilepathA;
            TempData["ContentType"] = ContentTypeA;
            int i = 1;
            return Json(i);
        }

        public FileResult DownloadDocuments(int id)
        {
            string DocName = Convert.ToString(TempData["DocName"]);
            string Filepath = Convert.ToString(TempData["FilePath"]);
            string ContentType = Convert.ToString(TempData["ContentType"]);
            byte[] fileBytes = System.IO.File.ReadAllBytes(Filepath);
            return File(fileBytes, ContentType, DocName);
        }

        public JsonResult GetDocuments(string DD)
        {
            List<BE.AddDocuments> list = new List<BE.AddDocuments>();
            list = DataProvider.GetDocuments(DD);
            return Json(list);
        }

        public ActionResult TripsCompleted()
        {
            return View();
        }

        public JsonResult GetBookingsTripsCompleted(BE.Bookings GDC)
        {
            List<BE.Bookings> list = new List<BE.Bookings>();
            list = DataProvider.GetBookingsTripsCompleted(GDC);
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

        public ActionResult Documents(string BookingNo)
        {
            ViewBag.BookingNo = BookingNo;
            return PartialView();
        }

        public ActionResult DeliveryAttachment()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AjaxUploadDeliveryAttachment(BE.Documents fileData)
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
                        string root = Server.MapPath("~/Uploads/Delivery/" + Type + "/");
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
                        string check = Path.Combine(Server.MapPath("~/Uploads/Delivery/" + Type + "/"), fname);
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

        public JsonResult AjaxDeliveryAttachment(string BN, List<BE.Documents> A)
        {
            int AddedBy = Convert.ToInt16(Session["UserId"]);
            BE.ResponseMessage message = new BE.ResponseMessage();

            if (A != null)
            {
                for (int j = 0; j < A.Count; j++)
                {
                    string root = Server.MapPath("~/Uploads/DeliveryAttachment/DeliveryAttachmentFor" + BN + "/");
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    string oldPath = A[j].FilePath;
                    string newPath = Path.Combine(Server.MapPath("~/Uploads/DeliveryAttachment/DeliveryAttachmentFor" + BN + "/"), A[j].DocName);
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
                dataTable.TableName = "PT_DeliveryAttachment";
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
                message = DataProvider.SaveDeliveryAttachment(dataTable, AddedBy);
            }
            return Json(message);
        }

        [HttpPost]
        public ActionResult ShowLorryAttachment(string BookingNo)
        {
            ViewBag.BookingNo = BookingNo;
            return PartialView();
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

        public JsonResult GetLorryAttachment(string LD)
        {
            List<BE.Documents> list = new List<BE.Documents>();
            list = DataProvider.GetLorryAttachment(LD);
            return Json(list);
        }

        [HttpPost]
        public ActionResult ShowOffloadingAttachment(string BookingNo)
        {
            ViewBag.BookingNo = BookingNo;
            return PartialView();
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

        public JsonResult GetOffloadingAttachment(string OD)
        {
            List<BE.Documents> list = new List<BE.Documents>();
            list = DataProvider.GetOffloadingAttachment(OD);
            return Json(list);
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

        public JsonResult AjaxPaymentBooking(string BookingNo)
        {
            string qrCodeUrl = "";
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(BookingNo, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    qrCodeUrl = string.Format("data:image/png;base64,{0}", Convert.ToBase64String(byteImage));
                    TempData["BookingNo"] = BookingNo;
                }
            }
            return Json(qrCodeUrl);
        }

        public JsonResult AjaxAddPayment()
        {
            BE.ResponseMessage message = new BE.ResponseMessage();
            string BookingNo = TempData["BookingNo"].ToString();
            message = DataProvider.AddPayment(BookingNo);
            return Json(message);
        }
    }
}