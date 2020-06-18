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
    public class EnrollController : Controller
    {
        EnrollModel em = new EnrollModel();
        [LogExceptions]
        public ActionResult GetAllCourseEnroll()
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                EnrollRepo ep = new EnrollRepo();
                return View(ep.GetAllCourseEnrollRepo());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [LogExceptions]
        public ActionResult AddEnrollDetails(int id)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                EnrollRepo EnrRepo = new EnrollRepo();
                return View(EnrRepo.GetAllCourseEnrollRepo().Find(enroll => enroll.courseId == id));
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        [LogExceptions]
        public ActionResult AddEnrollDetails(int id, EnrollModel obj)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    EnrollRepo EnrRepo = new EnrollRepo();

                    EnrRepo.AddEnrollRepo(obj, Session["UserId"]);

                    return RedirectToAction("GetAllCourseEnrolled");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


        [LogExceptions]
        public ActionResult GetAllCourseEnrolled()
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                EnrollRepo ep = new EnrollRepo();
                return View(ep.GetAllCourseEnrolledRepo(Session["Accountid"], Session["UserId"]));
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [LogExceptions]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult UpdateEnrollment(EnrollModel model)
        {
            try
            {
                EnrollRepo Lr = new EnrollRepo();
                EnrollModel model1 = Lr.dropdownrepo();
                return View("UpdateEnrollment", model1);
            }
            catch (SqlException)
            {
                ViewBag.duplicatemessage = "Please Enter All The Data";
                return RedirectToAction("UpdateEnrollment", model);
            }
        }

        [HttpPost]
        [Route("UpdateEnrollment")]
        [LogExceptions]
        public ActionResult UpdateEnrollment(EnrollModel model, FormCollection form)
        {
            try
            {
                EnrollRepo Lr = new EnrollRepo();
                if (Lr.UpdateEnrolledRepo(model, Session["Accountid"], form))
                {
                    ViewBag.SuccessMessage = "Enrollment Updated successfully";

                }
                return RedirectToAction("GetAllCourseEnrolled");

            }
            catch (SqlException)
            {
                ViewBag.duplicatemessage = "SQl Error";
                return RedirectToAction("GetAllCourseEnrolled");
            }

        }


        [LogExceptions]
        public ActionResult DeleteEnroll(int id)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    EnrollRepo EnrRepo = new EnrollRepo();
                    if (EnrRepo.DeleteEnrollment(id, Session["Accountid"]))
                    {
                        ViewBag.AlertMsg = "Enrollment deleted successfully";

                    }
                    return RedirectToAction("GetAllCourseEnrolled");

                }
                catch
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}
