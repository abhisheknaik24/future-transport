using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using BE = FutureTransportEntities.BusinessEntities;
using BL = FutureTransportBuisenessLayer.Driver;
using System.Data;
using FutureTransport.Filters;

namespace FutureTransport.Controllers.Driver
{
    [UserAuthenticationFilter]
    public class DriverController : Controller
    {
        BL.DriverDataProvider DataProvider = new BL.DriverDataProvider();
        public ActionResult DriverRegistration()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult AttachmentDriverRegistration()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AjaxUploadDriverLicense(BE.DriverLicense fileData)
        {
            BE.DriverLicense driverLicense = new BE.DriverLicense();
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
                        string root = Server.MapPath("~/Uploads/DriverLicense/" + Type + "/");
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
                        string check = Path.Combine(Server.MapPath("~/Uploads/DriverLicense/" + Type + "/"), fname);
                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }
                        file.SaveAs(check);
                        driverLicense.DocName = fname;
                        driverLicense.FilePath = check;
                        driverLicense.ContentType = contentType;
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            return Json(driverLicense);
        }

        [HttpPost]
        public ActionResult StoreFileDataInTemp(string DocName, string Filepath, string ContentType)
        {
            TempData["DocName"] = DocName;
            TempData["Filepath"] = Filepath;
            TempData["ContentType"] = ContentType;
            int i = 1;
            return Json(i);
        }

        public FileResult DownloadFile(int id)
        {
            string DocName = Convert.ToString(TempData["DocName"]);
            string Filepath = Convert.ToString(TempData["Filepath"]);
            string ContentType = Convert.ToString(TempData["ContentType"]);
            byte[] fileBytes = System.IO.File.ReadAllBytes(Filepath);
            return File(fileBytes, ContentType, DocName);
        }

        public JsonResult AjaxDriverRegistration(BE.Drivers DD, List<BE.DriverLicense> A)
        {
            DD.AddedBy = Convert.ToInt16(Session["UserId"]);
            List<BE.Drivers> drivers = new List<BE.Drivers>();
            drivers = DataProvider.DriverRegistration(DD);

            if (A != null)
            {
                for (int j = 0; j < A.Count; j++)
                {
                    string root = Server.MapPath("~/Uploads/DriverLicenseAttachment/DriverLicenseFor" + drivers[0].DriverNo + "/");
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    string oldPath = A[j].FilePath;
                    string newPath = Path.Combine(Server.MapPath("~/Uploads/DriverLicenseAttachment/DriverLicenseFor" + drivers[0].DriverNo + "/"), A[j].DocName);
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
                dataTable.Columns.Add("DriverNo");
                dataTable.Columns.Add("UploadFor");
                dataTable.Columns.Add("DocName");
                dataTable.Columns.Add("FilePath");
                dataTable.Columns.Add("ContentType");
                dataTable.TableName = "PT_DriverLicenseAttachment";
                foreach (BE.DriverLicense item in A)
                {
                    DataRow row = dataTable.NewRow();
                    row["DocId"] = item.DocId;
                    row["DriverNo"] = drivers[0].DriverNo;
                    row["UploadFor"] = item.UploadFor;
                    row["DocName"] = item.DocName;
                    row["FilePath"] = item.FilePath;
                    row["ContentType"] = item.ContentType;
                    dataTable.Rows.Add(row);
                }
                string message = DataProvider.SaveAttachment(dataTable, DD.AddedBy);
            }
            return Json(drivers);
        }

        public JsonResult GetDrivers()
        {
            List<BE.Drivers> list = new List<BE.Drivers>();
            list = DataProvider.GetDrivers();
            return Json(list);
        }
    }
}