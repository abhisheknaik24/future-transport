using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL = FutureTransportBuisenessLayer.ManageUser;
using BE = FutureTransportEntities.BusinessEntities;
using Newtonsoft.Json;
using System.Data;

namespace FutureTransport.Controllers.ManageUser
{
    public class ManageUserController : Controller
    {
        // GET: ManageUser
        BL.ManageUser manageUser = new BL.ManageUser();
        public ActionResult ManageUser()
        {
            long Userid = Convert.ToInt64(Session["Tracker_userID"]);
            ViewBag.Userid = Userid;
            //List<BE.ManageUser> User = new List<BE.ManageUser>();
            //User = manageUser.UserName();
            //ViewBag.User = new SelectList(User, "UserID", "USerName");

            return View();
        }

        public JsonResult GetUserDetails(string Userid)
        {
            List<BE.ManageUser> userList = new List<BE.ManageUser>();
            userList = manageUser.GetUserDetails(Userid);
            var JsonResult = Json(userList, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public JsonResult SaveUserDetails(List<BE.ManageUser> NewUser)
        {
            var message = "";
            long Userid = Convert.ToInt64(Session["Tracker_userID"]);

            message = manageUser.SaveUserDetails(NewUser, Userid);

            return Json(message);
        }

        public JsonResult AjaxCheckUsername(string Username)
        {
            var message = "";

            message = manageUser.AjaxCheckUsername(Username);

            return Json(message);
        }

        public ActionResult UserCompanyMapping()
        {
            long Userid = Convert.ToInt64(Session["Tracker_userID"]);

            List<BE.ManageUser> UserList = new List<BE.ManageUser>();
            UserList = manageUser.GetUserList(Userid);
            ViewBag.UserList = new SelectList(UserList, "Userid", "Username");

            List<BE.ManageUser> menuList = new List<BE.ManageUser>();
            menuList = manageUser.GetUserCompanyMapping(Userid);
            ViewBag.MenuList = menuList;

            return View(menuList.ToList());

        }

        public JsonResult GetUserCompanyDetails(string Userid)
        {
            long MainUserid = Convert.ToInt64(Session["Tracker_userID"]);
            List<BE.ManageUser> userList = new List<BE.ManageUser>();
            userList = manageUser.GetUserCompanyDetails(MainUserid,Userid);
            var JsonResult = Json(userList, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }

        public JsonResult getAssignedMenuForRole(string Userid)
        {
            long userid = Convert.ToInt64(Userid);
            List<BE.ManageUser> menuList = new List<BE.ManageUser>();
            menuList = manageUser.GetUserCompanyMapping(userid);
            var JsonResult = Json(menuList, JsonRequestBehavior.AllowGet);
            JsonResult.MaxJsonLength = int.MaxValue;
            return JsonResult;
        }
    }
}