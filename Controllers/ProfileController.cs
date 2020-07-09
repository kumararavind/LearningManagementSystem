using LMSProfile.ExceptionLogger;
using DataAccess;
using Business;
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
        [Route("ProfileDetails")]
        [LogExceptions]
        public ActionResult ProfileDetails() //can be seen in profile page and used to retrieve values inside textboxes.
        {
            ProfileModel model = new ProfileModel();
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    ProfileRepo ProRepo = new ProfileRepo();
                    ProRepo.GetProfile(model,Session["Accountid"], Session["UserId"]);
                }
                catch (SqlException)
                {
                    ViewData["Errormessage"] = "Error While Loading Profile.";
                    return View(model);
                }

                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpPost]
        [LogExceptions]
        [Route("ProfileDetails")]
        public ActionResult ProfileDetails(ProfileModel model) //used to update the profile page after clicking the update button in profile page.
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    ProfileRepo ProRepo = new ProfileRepo();
                    ProRepo.PostProfile(model, Session["Accountid"], Session["UserId"]);
                }
                catch (SqlException)
                {
                    ViewData["Errormessage"] = "Profile Updation Failed.";
                    return View(model);
                }
                ViewData["Successmessage"] = "Profile Updated Sucessfully.";
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            

        }
    }
}