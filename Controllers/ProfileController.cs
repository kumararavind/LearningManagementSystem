using LMSProfile.ExceptionLogger;
using LMSProfile.Models;
using LMSProfile.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSProfile.Controllers
{
    public class ProfileController : Controller
    {
        
        [LogExceptions]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult ProfileDetails(ProfileModel model) //can be seen in profile page and used to retrieve values inside textboxes.
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    ProfileRepo ProRepo = new ProfileRepo();
                    ProRepo.GetProfile(model,Session["Accountid"], Session["UserId"]);
                }
                catch (SqlException)
                {
                    ViewBag.duplicatemessage = "Error While Loading Profile";
                    return View("ProfileDetails", model);
                }

                return View("ProfileDetails", model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        [LogExceptions]
        [Route("ProfileDetails1")]
        public ActionResult ProfileDetails1(ProfileModel model) //used to update the profile page after clicking the update button in profile page.
        {
            try
            {
                ProfileRepo ProRepo = new ProfileRepo();
                ProRepo.PostProfile(model, Session["Accountid"], Session["UserId"]);
            }
            catch (SqlException)
            {
                ViewBag.duplicatemessage = "Profile Updation Failed";
                return RedirectToAction("ProfileDetails", model);
            }
            ViewBag.Successmessage = "Profile Updated";
            return RedirectToAction("ProfileDetails", model);

        }
    }
}