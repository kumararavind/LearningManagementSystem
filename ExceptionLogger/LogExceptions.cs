using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSProfile.ExceptionLogger
{
    public class LogExceptions : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            string message = "\n" + filterContext.RouteData.Values["Controller"].ToString() + "-->"
                + filterContext.RouteData.Values["action"].ToString() + "-->" + filterContext.Exception.Message + "\t at " + DateTime.Now.ToString() + "\n";
            logExceptions(message);
            logExceptions("-----------------------");
        }

        private void logExceptions(string data)
        {
            File.AppendAllText(HttpContext.Current.Server.MapPath("~/ExceptionData/Data.txt"), data);
        }
    }
}