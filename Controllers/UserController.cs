//using LMSProfile.Models;
//using LMSProfile.Repository;
using DataAccess;
using Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LMSProfile.ExceptionLogger;

namespace LMSProfile.Controllers
{
    public class UserController : Controller
    {
        [LogExceptions]
        public ActionResult GetAllUser()
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                UserRepo Repo = new UserRepo();
                ModelState.Clear();
                return View(Repo.GetAllUserRepo());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


        [LogExceptions]
        public ActionResult DeleteUser(int id)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    UserRepo Repo = new UserRepo();
                    if (Repo.DeleteUser(id))
                    {
                        ViewBag.SuccessMessage = "User details deleted successfully";

                    }

                }
                catch(SqlException)
                {
                    ViewBag.duplicatemessage = "One or more reference to this instrcutor is present so couldnt delete this record";
                    
                }
                return RedirectToAction("GetAllUser");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}
