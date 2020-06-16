using LMSProfile.ExceptionLogger;
using LMSProfile.Models;
using LMSProfile.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSProfile.Controllers
{
    public class CourseController : Controller
    {

        [LogExceptions]
        public ActionResult GetAllCourse()
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                CourseRepo Repo = new CourseRepo();
                ModelState.Clear();
                return View(Repo.GetAllCourse());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        [LogExceptions]
        public ActionResult AddCourse()
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                CourseRepo cour = new CourseRepo();
                CourseModel model=cour.dropdownrepo();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        [HttpPost]
        [LogExceptions]
        public ActionResult AddCourse(CourseModel model,FormCollection form)
        {
            try
            {
                CourseRepo CouRepo = new CourseRepo();

                if (CouRepo.AddCourse(model,form))
                {
                    ViewBag.Message = "Course details added successfully";
                }
                
                return RedirectToAction("Addcourse",model);
            }
            catch
            {
                ViewBag.Message = "Course details Addition Failed";
                return View();
            }
        }

        [LogExceptions]
        public ActionResult EditCouDetails(int id)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                CourseRepo CouRepo = new CourseRepo();
                return View(CouRepo.GetAllCourse().Find(Cou => Cou.courseId == id));
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
   
        [HttpPost]
        [LogExceptions]
        public ActionResult EditCouDetails(int id, CourseModel obj)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    CourseRepo CouRepo = new CourseRepo();

                    CouRepo.UpdateCourse(obj);

                    return RedirectToAction("GetAllCourse");
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
        public ActionResult DeleteCou(int id)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    CourseRepo CouRepo = new CourseRepo();
                    if (CouRepo.DeleteCourse(id))
                    {
                        ViewBag.AlertMsg = "Course details deleted successfully";

                    }
                    return RedirectToAction("GetAllCourse");

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
    }
}