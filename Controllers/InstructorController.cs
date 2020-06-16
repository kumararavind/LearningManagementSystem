using LMSProfile.ExceptionLogger;
using LMSProfile.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSProfile.Controllers
{
    public class InstructorController : Controller
    {
        [LogExceptions]
        public ActionResult GetAllInstructor()
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                InstructorRepo Repo = new InstructorRepo();
                ModelState.Clear();
                return View(Repo.GetAllInstructorRepo());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


        [LogExceptions]
        public ActionResult DeleteInst(int id)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    InstructorRepo Repo = new InstructorRepo();
                    if (Repo.DeleteInstructor(id))
                    {
                        ViewBag.SuccessMessage = "Instructor details deleted successfully";

                    }

                }
                catch (SqlException)
                {
                    ViewBag.duplicatemessage = "One or more reference to this instrcutor is present so couldnt delete this record";
                }
                return RedirectToAction("GetAllInstructor");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}
