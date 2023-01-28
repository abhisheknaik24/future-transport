using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace FutureTransport.Filters
{
    public class UserAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserId"])))
            {
                HttpCookie reqCookies = filterContext.HttpContext.Request.Cookies["UserCookies"];
                if (reqCookies.Domain == null)
                {
                    filterContext.Result = new RedirectResult("~/Home/Index");
                }
                else
                {
                    filterContext.HttpContext.Session["UserId"] = reqCookies["UserId"].ToString();
                    filterContext.HttpContext.Session["UserName"] = reqCookies["UserName"].ToString();
                    filterContext.HttpContext.Session["UserPass"] = reqCookies["UserPass"].ToString();
                    filterContext.HttpContext.Session["Department"] = reqCookies["Department"].ToString();
                    filterContext.HttpContext.Session["UserType"] = reqCookies["UserType"].ToString();
                    filterContext.HttpContext.Session["UserRight"] = reqCookies["UserRight"].ToString();
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
        }
    }
}