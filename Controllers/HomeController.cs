using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using BL = FutureTransportBuisenessLayer.Login;
using BE = FutureTransportEntities.BusinessEntities;

namespace FutureTransport.Controllers
{
    public class HomeController : Controller
    {
        BL.LoginDataProvider DataProvider = new BL.LoginDataProvider();
        public ActionResult Index()
        {
            BE.LoginEntites logindata = new BE.LoginEntites();
            string User_name = string.Empty;
            string User_color = string.Empty;

            HttpCookie reqCookies = Request.Cookies["UserInfo"];
            if (reqCookies != null)
            {
                logindata.UserName = reqCookies["UserName"].ToString();
                logindata.UserPass = reqCookies["UserPass"].ToString();
                logindata.RemeberMe = true;
            }
            return View("Index", logindata);
        }

        [HttpPost]
        public ActionResult Login(BE.LoginEntites loginEntities)
        {
            var name = loginEntities.UserName;
            var pass = loginEntities.UserPass;
            Boolean rememberme = Convert.ToBoolean(loginEntities.RemeberMe);

            BE.LoginEntites logindata = new BE.LoginEntites();
            logindata = DataProvider.LoginGetData(name, pass);
            if (logindata.UserId != 0)
            {
                Session["UserId"] = logindata.UserId;
                Session["UserName"] = logindata.UserName;
                Session["Department"] = logindata.Department;

                HttpCookie UserCookies = new HttpCookie("UserCookies");
                UserCookies["UserId"] = Convert.ToString(logindata.UserId);
                UserCookies["UserName"] = logindata.UserName;
                UserCookies["UserPass"] = logindata.UserPass;
                UserCookies["Department"] = logindata.Department;
                Response.Cookies.Add(UserCookies);

                RememberMe(name, pass, rememberme);
                if (logindata.Department == "admin")
                {
                    return RedirectToAction("AdminDashboard", "AdminDashboard");
                }
                else if(logindata.Department == "customer")
                {
                    return RedirectToAction("CustomerDashboard", "CustomerDashboard");
                }
                else if(logindata.Department == "vendor")
                {
                    return RedirectToAction("VendorDashboard", "VendorDashboard");
                }
                else
                {
                    logindata.LoginErrorMessage = "Wrong Username or Password.";
                    return View("Index", logindata);
                }
            }
            else
            {
                logindata.LoginErrorMessage = "Wrong Username or Password.";
                return View("Index", logindata);
            }
        }

        private void RememberMe(String name, String password, Boolean rememberme)
        {

            if (rememberme)
            {
                HttpCookie UserInfo = new HttpCookie("UserInfo");
                UserInfo["UserName"] = name;
                UserInfo["UserPass"] = password;
                UserInfo.Expires = DateTime.Now.AddDays(30);
                Response.Cookies.Add(UserInfo);
            }
            else
            {
                HttpCookie vms_userInfo = new HttpCookie("UserInfo");
                vms_userInfo.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(vms_userInfo);
            }
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult SideMenu()
        {
            int UserId = Convert.ToInt32(Session["UserId"]);
            List<BE.MenuModel> menuList = new List<BE.MenuModel>();
            menuList = DataProvider.GetSideMenu(UserId);
            Session["MenuList"] = menuList;
            return PartialView("SideMenu");
        }
    }
}